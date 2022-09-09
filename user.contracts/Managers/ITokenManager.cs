using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using user.entities.DTO;
using user.entities.Entities;

namespace user.contracts.Managers
{
    public interface ITokenManager
    {
        string GenerateAccessToken(UserDTO user);

        Task<string> GenerateRefreshToken(ApplicationUser applicationUser);

        Task<bool> ValidateRefreshToken(string token, string userId);

        bool ValidateToken(string token);
    }
}
