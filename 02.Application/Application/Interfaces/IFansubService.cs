using Domain.DTOs;
using Domain.Models;
using System;

namespace Application.Interfaces
{
    public interface IFansubService
    {
        Fansub Create(FansubDTO fansubDTO, Guid identityID);
        void Delete(Guid fansubID, Guid identityID);
        Fansub GetByID(Guid ID);
    }
}
