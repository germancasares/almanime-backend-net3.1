using Domain.DTOs;
using Domain.Models;
using System;

namespace Application.Interfaces
{
    public interface IFansubService
    {
        Fansub Create(FansubDTO fansubDTO);
        void Delete(Guid fansubID, Guid userTrigger);
    }
}
