using System;
using System.Collections.Generic;
using System.Text;
using user.business.Wrapper;
using user.contracts.Common;
using user.entities.Common;

namespace user.business.Mappers
{
    public class AccessTokenMapper : IMapper<AccessTokenMapperWrapper, TokenResponse>
    {
        public TokenResponse Map(AccessTokenMapperWrapper input)
        {
            return new TokenResponse
            {
                AccessToken = input.Token,
                RefreshToken = input.RefreshToken,
                User = new UserResponse
                {
                    Id = input.User.Id,
                    Email = input.User.Email,
                    FirstName = input.User.FirstName,
                    LastName = input.User.LastName,
                }
            };
        }
    }
}
