using Pointwest.Test.Classes.Utilities.Contants;
using Pointwest.Test.Interfaces.Converter;
using Pointwest.Test.Interfaces.Http;
using Pointwest.Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pointwest.Test.Classes.Helper.Http
{
    public class ApiContentProviderApplication : IApiContentProvider
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly ApplicationContantUtilities _applicationContantsUtilities;
        public ApiContentProviderApplication(IJsonConverter converter, ApplicationContantUtilities applicationContantsUtilities)
        {
            _jsonConverter = converter;
            _applicationContantsUtilities = applicationContantsUtilities;
        }
       
        public StringContent StringContent<TModel>(TModel model)
        {
            var content = new StringContent(_jsonConverter.ToJsonString(model), Encoding.UTF8, _applicationContantsUtilities.JsonContentType);           
                content.Headers.ContentType.CharSet = string.Empty;
                return content;
           
        }

    }
}
