using System;
using System.Collections.Generic;
using System.Text;
using user.contracts.Common;
using user.entities.Common;

namespace user.resources
{
    public class ErrorMessagesHandler : IErrorMessagesHandler
    {
        public Message GetServiceError()
        {
            return new Message
            {
                Code = "E0001",
                Description = ErrorMessages.ER0001
            };
        }

        public Message GetInvalidUserCredentialsError()
        {
            return new Message
            {
                Code = "E0002",
                Description = ErrorMessages.ER0002
            };
        }
        public Message GetUserAlreadyExistError()
        {
            return new Message
            {
                Code = "E0003",
                Description = ErrorMessages.ER0003
            };
        }

        public Message GetUserCreateError()
        {
            return new Message
            {
                Code = "E0004",
                Description = ErrorMessages.ER0004
            };
        }
    }
}
