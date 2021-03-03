using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Test.Interfaces.Converter
{
    public interface IJsonConverter
    {
        string ToJsonString<TModel>(TModel type);
    }
}
