using Application.Services.Interfaces;
using Domain.DTOs;
using Domain.Enums;
using Domain.Enums.Anime;
using Functions.Models;
using Infrastructure.Helpers;
using Kitsu.Anime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Functions
{
    public static class AnimeUpdater
    {
        private const int AnimesInPage = 20;
        private const string KitsuAPI = "https://kitsu.io/api/edge";
        private static readonly HttpClient Client = new HttpClient();
        private static ILogger Log;
        private static IAnimeService AnimeService;

        static AnimeUpdater()
        {
            AnimeService = DependencyInjection.Services.GetService<IAnimeService>();
        }

        [FunctionName("AnimeUpdater")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "PUT", Route = "AnimeUpdater/{year}/{season}")]
            HttpRequest req,
            int year,
            string season,
            ILogger log)
        {
            Log = log;

            if (year < 2000 || year > DateTime.Now.Year + 2) throw new ArgumentException("Year must be [2000, CurrentYear+2]");

            var seasonEnum = EnumHelper.GetEnumFromString<Season>(season);
            if (!seasonEnum.HasValue) throw new ArgumentException("Season must be {Winter, Spring, Summer, Fall}");

            var url = $"{KitsuAPI}/anime?filter[seasonYear]={year}&filter[season]={season.ToString().ToLower()}&page[limit]={AnimesInPage}";

            var animes = new List<KeyValuePair<string, AnimeAttributesModel>>();
            while (!string.IsNullOrWhiteSpace(url))
            {
                var (next, animesInPage) = await ProcessPage(url);
                animes.AddRange(animesInPage);
                url = next;
            }

            var animesDTOs = animes.Select(a => MapAnime(a.Key, a.Value)).Where(a => a.Status != Status.Tba && a.Season == seasonEnum).ToList();

            animesDTOs.ForEach(a => CreateOrUpdateAnime(a));

            return new OkResult();
        }

        private static async Task<(string next, IEnumerable<KeyValuePair<string, AnimeAttributesModel>> animesInPage)> ProcessPage(string url)
        {
            var httpResponse = await Client.GetAsync(url);
            var response = await httpResponse.Content.ReadAsStringAsync();
            var animeCollection = JsonConvert.DeserializeObject<AnimeCollection>(response);
            return (animeCollection.Links.Next, animeCollection.Data.Select(c => new KeyValuePair<string, AnimeAttributesModel>(c.Id, c.Attributes)));
        }

        private static AnimeDTO MapAnime(string kitsuID, AnimeAttributesModel anime)
        {
            // Status
            var status = EnumHelper.GetEnumFromString<Status>(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(anime.Status));
            if (!status.HasValue)
            {
                Log.LogError($"[AnimeUpdater~MapAnime(Status)] Anime {anime.Slug} - Status {anime.Status}");
                return null;
            }

            // StartDate
            if (!DateTime.TryParseExact(anime.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
            {
                Log.LogError($"[AnimeUpdater~MapAnime(StartDate)] Anime {anime.Slug} - StartDate {anime.StartDate}");
                return null;
            }

            // EndDate
            var endDateCorrect = DateTime.TryParseExact(anime.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

            // Season
            var season = startDate.GetSeason();
            if (!season.HasValue)
            {
                Log.LogError($"[AnimeUpdater~MapAnime(Season)] Anime {anime.Slug} - StartDate {anime.StartDate}");
                return null;
            }

            return new AnimeDTO
            {
                KitsuID = int.Parse(kitsuID),
                Slug = anime.Slug,
                Name = anime.CanonicalTitle,
                Episodes = anime.EpisodeCount,
                Synopsis = anime.Synopsis,
                Status = status.Value,
                StartDate = startDate,
                EndDate = endDateCorrect ? endDate : (DateTime?)null,
                Season = season.Value
            };
        }

        private static void CreateOrUpdateAnime(AnimeDTO anime)
        {
            if (AnimeService.GetByKitsuID(anime.KitsuID) == null)
            {
                AnimeService.Create(anime);
            }
            else
            {
                AnimeService.Update(anime);
            }
        }
    }
}