using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pointwest.Test.Interfaces.Http;
using Pointwest.Test.Model;
using Pointwest.Test.Classes.Utilities.Contants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Pointwest.Test.Model.ViewModels;
using System.Net;

namespace Pointwest.Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _serviceAXA;
        private ApplicationViewModel _applicationViewModel;

        public ApplicationController(IApplicationService serviceAXA)
        {
            _serviceAXA = serviceAXA;
        }

        [HttpPost("register")]
        public async Task<IActionResult> PostRegisterAsync([FromBody] Applicant applicant)
        {
            try
            {
                _applicationViewModel = await _serviceAXA.PostRegisterAsync<ApplicationViewModel, Applicant>(applicant);
                return Ok(_applicationViewModel);
            }
            catch (Exception ex)
            {
                _applicationViewModel.IsSuccess = false;
                _applicationViewModel.Message = ex.Message;

                return StatusCode(500, _applicationViewModel);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> PostUploadFileAsync(IFormFile fileUpload)
        {
            try
            {
                _applicationViewModel = await _serviceAXA.PostUploadFileAsync<ApplicationViewModel, IFormFile>(fileUpload);
                return Ok(_applicationViewModel);
            }
            catch (Exception ex)
            {
                _applicationViewModel.IsSuccess = false;
                _applicationViewModel.Message = ex.Message;

                return StatusCode(500, _applicationViewModel);
            }
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> PostScheduleAsync([FromBody] Schedule schedule, bool isToAutomate)
        {
            try
            {
                _applicationViewModel = await _serviceAXA.PostScheduleAsync<ApplicationViewModel, Schedule>(schedule, isToAutomate);
                return Ok(_applicationViewModel);
            }
            catch (Exception ex)
            {
                _applicationViewModel.IsSuccess = false;
                _applicationViewModel.Message = ex.Message;

                return StatusCode(500, _applicationViewModel);
            }
        }

        [HttpPost("test/register-endpoint")]
        public async Task<IActionResult> RegisterTestEndpoint([FromBody] Applicant applicant)
        {
            return Ok(applicant);
        }

        [HttpPost("test/upload-endpoint")]
        public async Task<IActionResult> UploadTestEndpoint([FromBody] FileUpload fileUpload)
        {
            return Ok(fileUpload);
        }

        [HttpPost("test/schedule-endpoint")]
        public async Task<IActionResult> ScheduleTestEndpoint([FromBody] Schedule schedule)
        {
            return Ok(schedule);
        }
    }
}
