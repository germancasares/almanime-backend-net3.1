using Application.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.Enums;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;

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

        public Fansub GetByID(Guid ID) => _unitOfWork.Fansubs.GetByID(ID);

        public Fansub GetByAcronym(string acronym) => _unitOfWork.Fansubs.GetByAcronym(acronym);

        public IEnumerable<Anime> GetAnimes(string acronym) => _unitOfWork.Animes.GetByFansub(acronym);

        public Fansub Create(FansubDTO fansubDTO, Guid identityID)
        {
            var user = _unitOfWork.Users.GetByIdentityID(identityID);
            if (user == null) throw new ArgumentException(nameof(identityID));

            var fansub = _unitOfWork.Fansubs.Create(_mapper.Map<Fansub>(fansubDTO));

            _unitOfWork.Memberships.Create(new Membership
            {
                FansubID = fansub.ID,
                UserID = user.ID,
                Role = EFansubRole.Founder
            });

            _unitOfWork.Save();

            return fansub;
        }

        public void Delete(Guid fansubID, Guid identityID)
        {
            var user = _unitOfWork.Users.GetByIdentityID(identityID);
            if (user == null) throw new ArgumentException(nameof(identityID));

            if (!_unitOfWork.Memberships.IsFounder(fansubID, user.ID)) return;

            _unitOfWork.Fansubs.DeleteMembers(fansubID);
            _unitOfWork.Fansubs.Delete(fansubID);
            _unitOfWork.Save();
        }
    }
}
