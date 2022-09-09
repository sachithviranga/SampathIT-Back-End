using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using user.entities.Common;
using user.entities.DTO;

namespace user.contracts.Managers
{
    public interface IUserManager
    {
        Task<ServiceResponse> Login(LoginDTO request);
        Task<ServiceResponse> Register(RegisterDTO request);
        Task<ServiceResponse> GetUsers();
    }
}
