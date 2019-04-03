using Domain.DTOs.Account;
using Domain.Models;
using System;

namespace Application.Interfaces
{
    public interface IUserService
    {
        User GetByIdentityID(Guid id);
        User GetByNickName(string name);

        User Create(UserDTO userDTO);
    }
}
