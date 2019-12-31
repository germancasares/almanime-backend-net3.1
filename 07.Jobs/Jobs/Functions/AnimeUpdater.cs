using Application.Services.Interfaces;
using Jobs.Models;
using Infrastructure.Helpers;
using Kitsu.Anime;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Enums.Anime;
using Domain.DTOs;

namespace Jobs.Functions
{
    public class AnimeUpdater
    {
        private const int MAX_PER_PAGE = 20;
        private ILogger Log;
        private static IAnimeService _animeService;
        private const string KitsuAPI = "https://kitsu.io/api/edge";
        private static readonly string AnimeURL = $"{{0}}/anime?filter[seasonYear]={{1}}&filter[season]={{2}}&page[limit]={{3}}";
        private static readonly string EpisodeURL = $"{{0}}/anime/{{1}}/episodes?page[limit]={{2}}";
        private readonly HttpClient Client = new HttpClient();

        public AnimeUpdater(IAnimeService animeService)
        {
            _animeService = animeService;
        }

        [FunctionName("AnimeUpdater")]
        public async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "PUT", Route = "AnimeUpdater/{year}/{season}")]
            HttpRequest req,
            int year,
            string season,
            ILogger log)
        {
            Log = log;

            if (year < 2000 || year > DateTime.Now.Year + 2) throw new ArgumentException("Year must be [2000, CurrentYear+2]");

            var seasonEnum = EnumHelper.GetEnumFromString<ESeason>(season);
            if (!seasonEnum.HasValue) throw new ArgumentException("Season must be {Winter, Spring, Summer, Fall}");

            // Get AnimesDataModels
            var animes = await GetAnimes(year, seasonEnum.Value);

            // Get EpisodeDataModels
            var episodesTask = animes.Select(async c => new { c.Id, Episodes = await GetEpisodes(c) });
            var episodes = await Task.WhenAll(episodesTask.ToArray());

            // Map AnimeDataModels
            var animesDTOs = animes
                .Select(a => MapAnime(a))
                .Where(a => a != null && a.Status != EAnimeStatus.Tba && a.Season == seasonEnum)
                .ToList();

            // Map EpisodeDataModels
            var episodesDTOs = episodes
                .Select(c => new KeyValuePair<int, IEnumerable<EpisodeDTO>>(int.Parse(c.Id), c.Episodes.Select(x => MapEpisode(x))))
                .ToList();

            animesDTOs.ForEach(a =>
            {
                // Merge EpisodeDTOs into AnimeDTOs
                a.Episodes = episodesDTOs.SingleOrDefault(c => c.Key == a.KitsuID).Value;

                // Foreach CreateOrUpdateAnime
                CreateOrUpdateAnime(a);
            });
        }

        private async Task<List<AnimeDataModel>> GetAnimes(int year, ESeason season)
        {
            var animeDataModels = new List<AnimeDataModel>();

            var url = string.Format(AnimeURL, KitsuAPI, year, season.ToString().ToLower(), MAX_PER_PAGE);
            while (!string.IsNullOrWhiteSpace(url))
            {
                var (next, rawAnimes) = await ProcessAnimePage(url);

                var filteredAnimes = rawAnimes.Where(c => c.Attributes.Subtype == "TV").ToList();

                animeDataModels.AddRange(filteredAnimes);


                url = next;
            }

            return animeDataModels;
        }

        private async Task<List<EpisodeDataModel>> GetEpisodes(AnimeDataModel anime)
        {
            var episodeDataModels = new List<EpisodeDataModel>();

            var url = string.Format(EpisodeURL, KitsuAPI, anime.Id, MAX_PER_PAGE);
            while (!string.IsNullOrWhiteSpace(url))
            {
                var (next, rawEpisodes) = await ProcessEpisodePage(url);
                episodeDataModels.AddRange(rawEpisodes);
                url = next;
            }

            return episodeDataModels;
        }

        private async Task<(string Next, IEnumerable<AnimeDataModel> AnimeDataModel)> ProcessAnimePage(string url)
        {
            var response = await Client.GetStringAsync(url);
            var animeCollection = JsonConvert.DeserializeObject<AnimeCollection>(response);
            return (animeCollection.Links.Next, animeCollection.Data);
        }

        private async Task<(string Next, List<EpisodeDataModel> EpisodeDataModel)> ProcessEpisodePage(string url)
        {
            var response = await Client.GetStringAsync(url);
            var episodeCollection = JsonConvert.DeserializeObject<EpisodeCollection>(response, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
            return (episodeCollection.Links.Next, episodeCollection.Data);
        }

        private AnimeDTO MapAnime(AnimeDataModel model)
        {
            var anime = model.Attributes;

            if (!IsProcessable(model.Id , anime)) return null;

            var status = EnumHelper.GetEnumFromString<EAnimeStatus>(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(anime.Status));
            DateTime.TryParseExact(anime.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);

            var endDateCorrect = DateTime.TryParseExact(anime.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

            return new AnimeDTO
            {
                KitsuID = int.Parse(model.Id),
                Slug = anime.Slug,
                Name = anime.CanonicalTitle,
                Synopsis = anime.Synopsis,
                Status = status.Value,
                StartDate = startDate,
                EndDate = endDateCorrect ? endDate : (DateTime?)null,
                Season = EnumHelper.GetSeason(startDate.Month),
                CoverImageUrl = anime.CoverImage?.Original,
                PosterImageUrl = anime.PosterImage?.Original
            };
        }

        private static EpisodeDTO MapEpisode(EpisodeDataModel model)
        {
            var episode = model.Attributes;

            var endDateCorrect = DateTime.TryParseExact(episode.Airdate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime airDate);

            return new EpisodeDTO
            {
                Aired = endDateCorrect ? airDate : (DateTime?)null,
                Duration = episode.Length,
                Name = episode.CanonicalTitle,
                Number = episode.Number.Value,
            };
        }

        private bool IsProcessable(string id, AnimeAttributesModel anime)
        {
            // Slug
            if (anime.Slug == "delete")
            {
                Log.LogError($"[AnimeUpdater~MapAnime(Slug)] AnimeID {id} - Slug {anime.Slug}");
                return false;
            }

            // Status
            var status = EnumHelper.GetEnumFromString<EAnimeStatus>(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(anime.Status));
            if (!status.HasValue)
            {
                Log.LogError($"[AnimeUpdater~MapAnime(Status)] Anime {anime.Slug} - Status {anime.Status}");
                return false;
            }

            // StartDate
            if (!DateTime.TryParseExact(anime.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                Log.LogError($"[AnimeUpdater~MapAnime(StartDate)] Anime {anime.Slug} - StartDate {anime.StartDate}");
                return false;
            }

            return true;
        }

        private static void CreateOrUpdateAnime(AnimeDTO anime)
        {
            if (_animeService.GetByKitsuID(anime.KitsuID) == null)
            {
                _animeService.Create(anime);
            }
            else
            {
                _animeService.Update(anime);
            }
        }
    }
}