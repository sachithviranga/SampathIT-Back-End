using System;
using System.Collections.Generic;
using System.Text;
using user.contracts.Common;
using user.entities.Common;

namespace user.business.Mappers.Common
{
    public class ServiceResponseErrorMapper : IMapper<IList<Message>, ServiceResponse>
    {
        public ServiceResponse Map(IList<Message> input) => new ServiceResponse
        {
            IsError = true,
            Messages = input
        };
    }
}
