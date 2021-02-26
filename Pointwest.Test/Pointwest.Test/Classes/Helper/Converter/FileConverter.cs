using Microsoft.AspNetCore.Http;
using Pointwest.Test.Interfaces.Converter;
using Pointwest.Test.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Test.Classes.Helper.Converter
{
    public class FileConverter : IFileConverter
    {
        public async Task<string> ToBase64String(IFormFile formFile)
        {
            using var targetStream = new MemoryStream();
            await formFile.OpenReadStream().CopyToAsync(targetStream);
            var data = targetStream.ToArray();
            return Convert.ToBase64String(data);
        }

    }
}
