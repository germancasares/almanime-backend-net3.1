using Application.Interfaces;
using Domain.Models;
using Persistence.Data;
using System;
using Domain.DTOs;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Infrastructure.Helpers;
using Domain.Enums;

namespace Application
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUnitOfWork unitOfWork,
            ILogger<UserService> logger
            )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public User GetByIdentityID(Guid id) => _unitOfWork.Users.GetByIdentityID(id);
        public User GetByName(string name) => _unitOfWork.Users.GetByName(name);
        public bool ExistsName(string name) => GetByName(name) != null;

        public Task<User> Create(UserDTO userDTO, Guid identityID)
        {
            if (_unitOfWork.Users.GetByIdentityID(identityID) != null)
            {
                _logger.Emit(ELoggingEvent.UserAlreadyExists, new { IdentityID = identityID });
                throw new ArgumentException(nameof(identityID));
            }

            return CreateInternal(userDTO, identityID);
        }
        private async Task<User> CreateInternal(UserDTO userDTO, Guid identityID)
        {
            var userEntity = new User
            {
                IdentityID = identityID,
                Name = userDTO.Name
            };

            var user = _unitOfWork.Users.Create(userEntity);

            // If this fails, we don't upload an avatar for nothing.
            _unitOfWork.Save();

            if (userDTO.Avatar != null)
            {
                try
                {
                    user.AvatarUrl = await _unitOfWork.Storage.UploadAvatar(userDTO.Avatar, user.ID);
                    _unitOfWork.Users.Update(user);
                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    _logger.Emit(ELoggingEvent.CantUploadAvatar, new { IdentityID = identityID, Exception = ex });
                    return user;
                }
            }

            return user;
        }

        public Task Update(UserDTO userDTO, Guid identityID)
        {
            var user = _unitOfWork.Users.GetByIdentityID(identityID);
            if (user == null)
            {
                _logger.Emit(ELoggingEvent.UserDoesntExist, new { UserIdentityID = identityID });
                throw new ArgumentException(nameof(identityID));
            }

            return UpdateInternal(userDTO, user);
        }
        public async Task UpdateInternal(UserDTO userDTO, User user)
        {
            if (!string.IsNullOrWhiteSpace(userDTO.Name))
            {
                user.Name = userDTO.Name;
                _unitOfWork.Users.Update(user);
            }

            if (userDTO.Avatar != null)
            {
                user.AvatarUrl = await _unitOfWork.Storage.UploadAvatar(userDTO.Avatar, user.ID);
                _unitOfWork.Users.Update(user);
            }

            // We do not want to Update if UserDTO is empty.

            _unitOfWork.Save();
        }
    }
}
