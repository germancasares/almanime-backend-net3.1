using Domain.DTOs;
using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        User GetByIdentityID(Guid id);
        User GetByName(string name);

        Task<User> Create(UserDTO userDTO, Guid identityID);
        Task Update(UserDTO userDTO, Guid identityID);
        bool ExistsName(string name);
    }
}
