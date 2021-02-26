using Microsoft.AspNetCore.Http;
using Pointwest.Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Test.Interfaces.Converter
{
    public interface IFileConverter
    {
        public Task<string> ToBase64String(IFormFile formFile);
    }
}
