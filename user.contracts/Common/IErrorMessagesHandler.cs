using System;
using System.Collections.Generic;
using System.Text;
using user.entities.Common;

namespace user.contracts.Common
{
    public interface IErrorMessagesHandler
    {
        Message GetServiceError();
        Message GetInvalidUserCredentialsError();
        Message GetUserAlreadyExistError();
        Message GetUserCreateError();
    }
}
