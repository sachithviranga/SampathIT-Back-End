using System;
using System.Collections.Generic;
using System.Text;

namespace user.contracts.Common
{
    public interface IMapper<in TInput, out TOutput>
    {
        TOutput Map(TInput input);
    }
}
