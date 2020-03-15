using Domain.DTOs;
using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISubtitleService
    {
        Task<Subtitle> Create(SubtitleDTO subtitleDTO, string fansubAcronym, string animeSlug, int episodeNumber, Guid identityID);
        Subtitle GetByID(Guid ID);
        Subtitle GetForEdit(string fansubAcronym, string animeSlug, int episodeNumber, Guid identityID);
    }
}
