using System;
using System.Collections.Generic;
using System.Text;

namespace user.entities.Common
{
    public class ServiceResponse
    {
        public Object ReturnObject { get; set; }
        public bool IsError { get; set; }
        public IList<Message> Messages { get; set; }
    }
}
