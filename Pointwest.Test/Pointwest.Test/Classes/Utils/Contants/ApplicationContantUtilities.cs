using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Test.Classes.Utilities.Contants
{
    public class ApplicationContantUtilities
    {
        private readonly IConfiguration _configuration;
        public readonly string AXAContentType;
        public readonly string JsonContentType;
        public readonly string ApiKey;
        public readonly string UriBaseAXA;
        public readonly string UriBaseTest;
        public readonly string RegisterEnpoint;
        public readonly string UploadResumeEndpoint;
        public readonly string ScheduleEndpoint;
        public readonly string RegisterTestEndpoint;
        public readonly string UploadTestEndpoint;
        public readonly string ScheduleTestEndpoint;

        public ApplicationContantUtilities(IConfiguration configuration)
        {            
            _configuration = configuration;
            AXAContentType = _configuration["AXA:ApiConfiguration:CustomContentType"];
            JsonContentType = _configuration["AXA:ApiConfiguration:JsonContentType"];
            ApiKey = _configuration["AXA:ApiConfiguration:ApiKey"];
            UriBaseAXA = _configuration["AXA:UriBase:DevExam"];
            UriBaseTest = _configuration["Local:UriBase"];
            RegisterEnpoint = _configuration["AXA:Endpoints:Register"];
            UploadResumeEndpoint = _configuration["AXA:Endpoints:UploadResume"];
            ScheduleEndpoint = _configuration["AXA:Endpoints:Schedule"];
            RegisterTestEndpoint = _configuration["Local:Endpoints:Register"];
            UploadTestEndpoint = _configuration["Local:Endpoints:UploadResume"];
            ScheduleTestEndpoint = _configuration["Local:Endpoints:Schedule"];
        }
    }
}
