using System;
using System.Collections.Generic;
using System.Text;

namespace user.entities.Common.Configuration
{
    public class JwtConfiguration
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
        public int Expires { get; set; }
    }
}
