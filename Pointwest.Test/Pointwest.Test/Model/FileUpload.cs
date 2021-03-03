using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Test.Model
{
    public class FileUpload
    {
        [Required]
        public File File { get; set; }
    }

    public class File
    {
        public string Mime { get; set; }
        public string Data { get; set; }
    }
}
