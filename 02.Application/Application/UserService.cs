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
        public User GetByNickName(string nickName) => _unitOfWork.Users.GetByNickName(nickName);
        public bool ExistsNickName(string nickName) => GetByNickName(nickName) != null;

        public async Task<User> Create(UserDTO userDTO, Guid identityID)
        {
            if (_unitOfWork.Users.GetByIdentityID(identityID) != null) throw new ArgumentException(nameof(identityID));

            var userEntity = new User
            {
                IdentityID = identityID,
                NickName = userDTO.NickName
            };

            // TODO: What happens if UnitOfWork fails? we have this image that has to be deleted?
            if (userDTO.Avatar != null)
            {
                userEntity.AvatarUrl = await _unitOfWork.Images.UploadAvatar(userDTO.Avatar, userEntity.ID);
            }

            var user = _unitOfWork.Users.Create(userEntity);

            _unitOfWork.Save();

            return user;
        }

        public async Task Update(UserDTO userDTO, Guid identityID)
        {
            var user = _unitOfWork.Users.GetByIdentityID(identityID);

            if (user == null) throw new ArgumentException(nameof(identityID));

            if (!string.IsNullOrWhiteSpace(userDTO.NickName))
            {
                user.NickName = userDTO.NickName;
            }

            if (userDTO.Avatar != null)
            {
                user.AvatarUrl = await _unitOfWork.Images.UploadAvatar(userDTO.Avatar, user.ID);
            }

            _unitOfWork.Users.Update(user);
            _unitOfWork.Save();
        }
    }
}
