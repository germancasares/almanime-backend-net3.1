using Application.Interfaces;
using Domain.Models;
using Persistence.Data;
using System;
using Domain.DTOs.Account;
using AutoMapper;

namespace Application
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public User GetByIdentityID(Guid id) => _unitOfWork.Users.GetByIdentityID(id);
        public User GetByNickName(string name) => _unitOfWork.Users.GetByNickName(name);

        public User Create(UserDTO userDTO)
        {
            var user = _unitOfWork.Users.Create(_mapper.Map<UserDTO, User>(userDTO));

            _unitOfWork.Save();

            return user;
        }
    }
}
