using System;
using System.Collections.Generic;
using System.Text;

namespace user.contracts.Common
{
    public interface IEntityMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
