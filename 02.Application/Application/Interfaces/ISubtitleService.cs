using Domain.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISubtitleService
    {
        Task<Subtitle> Create(SubtitleDTO subtitleDTO, Guid identityID);
        Domain.Models.Subtitle GetByID(Guid ID);
    }
}
