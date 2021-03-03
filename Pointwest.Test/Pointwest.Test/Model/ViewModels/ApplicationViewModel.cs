using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Pointwest.Test.Dtos;

namespace Pointwest.Test.Model.ViewModels
{
    public class ApplicationViewModel
    {
        public FileUploadDto FileUploadDto { get; set; }
        //public FileUpload FileUpload { get; set; }
        public object Model { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
    }
}
