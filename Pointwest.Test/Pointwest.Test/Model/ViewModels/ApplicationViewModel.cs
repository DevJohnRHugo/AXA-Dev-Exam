using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Pointwest.Test.Model.ViewModels
{
    public class ApplicationViewModel
    {       
        public FileUpload FileUpload { get; set; }
        public object Model { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
    }
}
