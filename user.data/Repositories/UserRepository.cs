using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using user.contracts.Common;
using user.contracts.Repositories;
using user.data.DatabaseContext;
using user.entities.DTO;
using user.entities.Entities;

namespace user.data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbConext _dbContext;

        private readonly IEntityMapper _entityMapper;


        public UserRepository(UserDbConext dbContext, IEntityMapper entityMapper)
        {
            _dbContext = dbContext;
            _entityMapper = entityMapper;
        }

        public UserDTO GetUserById(string userId)
        {
            var userObj = new UserDTO();
            var user = _dbContext.Users.FirstOrDefault(a => a.Id == userId);
            if (user != null)
            {
                userObj = _entityMapper.Map<ApplicationUser, UserDTO>(user);
            }
            return userObj;
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            var user = await _dbContext.Users.ToListAsync();
            return _entityMapper.Map<List<ApplicationUser>, List<UserDTO>>(user);
        }
    }
}
