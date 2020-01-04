using Application.Interfaces;
using Domain.Models;
using Persistence.Data;
using System;
using Domain.DTOs;
using System.Threading.Tasks;

namespace Application
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }

        public User GetByIdentityID(Guid id) => _unitOfWork.Users.GetByIdentityID(id);
        public User GetByName(string name) => _unitOfWork.Users.GetByName(name);
        public bool ExistsName(string name) => GetByName(name) != null;

        public Task<User> Create(UserDTO userDTO, Guid identityID)
        {
            if (_unitOfWork.Users.GetByIdentityID(identityID) != null) throw new ArgumentException(nameof(identityID));

            return CreateInternal(userDTO, identityID);
        }
        private async Task<User> CreateInternal(UserDTO userDTO, Guid identityID)
        {
            var userEntity = new User
            {
                IdentityID = identityID,
                Name = userDTO.Name
            };

            // TODO: What happens if UnitOfWork fails? we have this image that has to be deleted?
            if (userDTO.Avatar != null)
            {
                userEntity.AvatarUrl = await _unitOfWork.Storage.UploadAvatar(userDTO.Avatar, userEntity.ID);
            }

            var user = _unitOfWork.Users.Create(userEntity);

            _unitOfWork.Save();

            return user;
        }

        public Task Update(UserDTO userDTO, Guid identityID)
        {
            var user = _unitOfWork.Users.GetByIdentityID(identityID);
            if (user == null) throw new ArgumentException(nameof(identityID));

            return UpdateInternal(userDTO, user);
        }
        public async Task UpdateInternal(UserDTO userDTO, User user)
        {
            if (!string.IsNullOrWhiteSpace(userDTO.Name))
            {
                user.Name = userDTO.Name;
            }

            if (userDTO.Avatar != null)
            {
                user.AvatarUrl = await _unitOfWork.Storage.UploadAvatar(userDTO.Avatar, user.ID);
            }

            _unitOfWork.Users.Update(user);
            _unitOfWork.Save();
        }
    }
}
