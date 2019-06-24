using Application.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.Enums;
using Domain.Models;
using Persistence.Data;
using System;

namespace Application
{
    public class FansubService : IFansubService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FansubService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Fansub Create(FansubDTO fansubDTO)
        {
            var fansub = _unitOfWork.Fansubs.Create(_mapper.Map<Fansub>(fansubDTO));

            _unitOfWork.Memberships.Create(new Membership
            {
                FansubID = fansub.ID,
                UserID = fansubDTO.Founder,
                Role = EFansubRole.Founder
            });

            _unitOfWork.Save();

            return fansub;
        }

        public void Delete(Guid fansubID, Guid userTrigger)
        {
            if (!_unitOfWork.Memberships.IsFounder(fansubID, userTrigger)) return;

            _unitOfWork.Fansubs.DeleteMembers(fansubID);
            _unitOfWork.Fansubs.Delete(fansubID);
            _unitOfWork.Save();
        }
    }
}
