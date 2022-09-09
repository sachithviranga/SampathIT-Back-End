using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using user.entities.DTO;

namespace user.contracts.Repositories
{
    public interface IUserRepository
    {
        UserDTO GetUserById(string userId);

        Task<List<UserDTO>> GetUsers();
    }
}
