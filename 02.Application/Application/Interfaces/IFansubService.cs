using Domain.DTOs;
using Domain.Models;
using Domain.VMs;
using System;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IFansubService
    {
        Fansub Create(FansubDTO fansubDTO, Guid identityID);
        void Delete(Guid fansubID, Guid identityID);
        IEnumerable<FansubAnimeVM> GetCompletedAnimes(string acronym);
        Fansub GetByAcronym(string acronym);
        Fansub GetByID(Guid ID);
        bool ExistsFullName(string fullname);
        bool ExistsAcronym(string acronym);
        IEnumerable<FansubEpisodeVM> GetCompletedEpisodes(string acronym);
        IEnumerable<FansubUserVM> GetMembers(string acronym);
    }
}
