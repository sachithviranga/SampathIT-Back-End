using System;
using System.Collections.Generic;
using System.Text;
using user.contracts.Common;
using user.entities.Common;

namespace user.business.Mappers.Common
{
    public class ServiceResponseMapper : IMapper<Object, ServiceResponse>
    {
        public ServiceResponse Map(object input)
        {
            return new ServiceResponse
            {
                ReturnObject = input,
                IsError = false
            };
        }
    }
}
