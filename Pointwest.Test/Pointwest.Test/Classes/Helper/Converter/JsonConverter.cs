using Newtonsoft.Json;
using Pointwest.Test.Interfaces.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Test.Classes.Helper.Converter
{
    public class JsonConverter : IJsonConverter
    {
        public string ToJsonString<TTYpe>(TTYpe type)
        {
            return JsonConvert.SerializeObject(type);
        }
    }
}
