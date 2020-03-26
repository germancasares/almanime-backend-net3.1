using Domain.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IFansubService
    {
        Fansub Create(FansubDTO fansubDTO, Guid identityID);
        void Delete(Guid fansubID, Guid identityID);
        IEnumerable<Anime> GetAnimes(string acronym);
        Fansub GetByAcronym(string acronym);
        Fansub GetByID(Guid ID);
        bool ExistsFullName(string fullname);
        bool ExistsAcronym(string acronym);
    }
}
