using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Test.Model
{
    public class FileUpload
    {
        public File File { get; set; }
    }

    public class File
    {
        public string Mime { get; set; }
        public string Data { get; set; }
    }
}
