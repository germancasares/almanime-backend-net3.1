using Application.Interfaces;
using Domain.Models;
using Persistence.Data;
using System;

namespace Application
{
    public class SubtitleService : ISubtitleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubtitleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Subtitle GetByID(Guid ID) => _unitOfWork.Subtitles.GetByID(ID);
    }
}
