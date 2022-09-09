using System;
using System.Collections.Generic;
using System.Text;
using user.business.Wrapper;
using user.contracts.Common;
using user.entities.Entities;

namespace user.business.Mappers
{
    public class UserCreateMapper : IMapper<UserCreateMapperWrapper, ApplicationUser>
    {
        public ApplicationUser Map(UserCreateMapperWrapper input)
        {
            return new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = input.User.FirstName,
                LastName = input.User.LastName,
                Email = input.User.Email,
                PhoneNumber = input.User.PhoneNumber,
                IsActive = true,
                UserName = input.User.Email,
                CreatedDate = DateTime.Now,
            };
        }
    }
}
