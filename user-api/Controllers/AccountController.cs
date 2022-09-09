using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user.contracts.Managers;
using user.entities.Common;
using user.entities.DTO;

namespace user_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost, Route("Login")]
        [AllowAnonymous]
        public async Task<ServiceResponse> Login([FromBody] LoginDTO request)
        {
            return await _userManager.Login(request);
        }

        [HttpPost, Route("Register")]
        [AllowAnonymous]
        public async Task<ServiceResponse> Register([FromBody] RegisterDTO request)
        {
            return await _userManager.Register(request);
        }
    }
}
