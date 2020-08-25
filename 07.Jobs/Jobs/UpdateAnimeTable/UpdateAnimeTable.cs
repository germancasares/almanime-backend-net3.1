using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Interfaces;
using Azure.Storage.Queues;
using Domain.DTOs;
using Domain.Enums;
using Infrastructure.Helpers;
using Jobs.UpdateEpisodeTable.Contracts;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using static Jobs.UpdateEpisodeTable.UpdateEpisodeTable;
using Jobs.Models.Anime;

namespace Jobs.UpdateAnimeTable
{
    public class UpdateAnimeTable
    {
        private const int MAX_PER_PAGE = 20;
        private const string KitsuAPI = "https://kitsu.io/api/edge";
        private static readonly string AnimeURL = $"{{0}}/anime?filter[seasonYear]={{1}}&filter[season]={{2}}&page[limit]={{3}}";
        private static readonly HttpClient Client = new HttpClient();

        private readonly IAnimeService _animeService;
        private readonly ILogger<UpdateAnimeTable> _logger;

        public UpdateAnimeTable(
            IAnimeService animeService,
            ILogger<UpdateAnimeTable> logger
            )
        {
            _animeService = animeService;
            _logger = logger;
        }

        [FunctionName("UpdateAnimeTable")]
        public async Task Run([TimerTrigger("0 8 * * * *", RunOnStartup = true)]TimerInfo myTimer)
        {
            var date = DateTime.Now;
            var year = date.Year;
            var season = EnumHelper.GetSeason(date);

            var animes = await GetAnimes(year, season);
            
            var animeMessages = animes.Select(a => new CAnime { ID = a.Id, Slug = a.Attributes.Slug }).ToList();
            await SendAnimeMessages(animeMessages);

            var animesDTOs = animes
                .Select(a => MapAnime(a))
                .Where(a => a != null && a.Status != EAnimeStatus.Tba && a.Season == season)
                .ToList();

            animesDTOs.ForEach(a => CreateOrUpdateAnime(a));
        }

        private static async Task SendAnimeMessages(List<CAnime> messages)
        {
            var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            var queue = new QueueClient(connectionString, UpdateEpisodeTableQueue);
            await queue.CreateIfNotExistsAsync();

            var sendTasks = messages.Select(async message => await queue.SendMessageAsync(message.ToString()));

            await Task.WhenAll(sendTasks.ToArray());
        }

        private static async Task<List<AnimeDataModel>> GetAnimes(int year, ESeason season)
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

        private static async Task<(string Next, IEnumerable<AnimeDataModel> AnimeDataModel)> ProcessAnimePage(string url)
        {
            var response = await Client.GetStringAsync(url);
            var animeCollection = JsonSerializer.Deserialize<AnimeCollection>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true,  });
            return (animeCollection.Links.Next, animeCollection.Data);
        }

        private AnimeDTO MapAnime(AnimeDataModel model)
        {
            var anime = model.Attributes;

            if (!IsProcessable(model.Id, anime)) return null;

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
                CoverImageUrl = GetBaseUrl(model.Id, anime.CoverImage?.Original),
                PosterImageUrl = GetBaseUrl(model.Id, anime.PosterImage?.Original),
            };
        }

        private bool IsProcessable(string id, AnimeAttributesModel anime)
        {
            // Slug
            if (anime.Slug == "delete")
            {
                _logger.Emit(ELoggingEvent.SlugIsDelete, new { AnimeID = id });
                return false;
            }

            // Status
            var status = EnumHelper.GetEnumFromString<EAnimeStatus>(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(anime.Status));
            if (!status.HasValue)
            {
                _logger.Emit(ELoggingEvent.AnimeStatusNotInRange, new { AnimeSlug = anime.Slug, AnimeStatus = anime.Status });
                return false;
            }

            // StartDate
            if (!DateTime.TryParseExact(anime.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                _logger.Emit(ELoggingEvent.StartDateNotRecognized, new { AnimeSlug = anime.Slug, AnimeStartDate = anime.StartDate });
                return false;
            }

            return true;
        }

        private static string GetBaseUrl(string id, string url) => url?.Substring(0, url.IndexOf(id) + id.Length + 1);

        private void CreateOrUpdateAnime(AnimeDTO anime)
        {
            if (_animeService.GetByKitsuID(anime.KitsuID) == null)
            {
                _animeService.Create(anime);
                _logger.Emit(ELoggingEvent.AnimeCreated, new { AnimeSlug = anime.Slug });
            }
            else
            {
                _animeService.Update(anime);
                _logger.Emit(ELoggingEvent.AnimeUpdated, new { AnimeSlug = anime.Slug });
            }
        }
    }
}
