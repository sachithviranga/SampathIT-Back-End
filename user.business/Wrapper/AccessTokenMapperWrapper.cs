using System;
using System.Collections.Generic;
using System.Text;
using user.entities.DTO;

namespace user.business.Wrapper
{
    public class AccessTokenMapperWrapper
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
