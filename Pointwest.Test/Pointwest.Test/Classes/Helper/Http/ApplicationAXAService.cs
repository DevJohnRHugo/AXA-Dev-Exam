using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pointwest.Test.Classes.Utilities.Contants;
using Pointwest.Test.Interfaces.Converter;
using Pointwest.Test.Interfaces.Http;
using Pointwest.Test.Model;
using Pointwest.Test.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pointwest.Test.Classes.Helper.Http
{
    public class ApplicationAXAService : IApplicationService
    {
        private readonly ApplicationContantUtilities _contantUtilities;
        private readonly IApiContentProvider _apiContentProvider;
        private ApplicationViewModel _applicationViewModel;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IFileConverter _fileConverter;
        private HttpResponseMessage _response;

        public ApplicationAXAService(IApiContentProvider apiContentProvider, IHttpClientFactory httpClientFactory, ApplicationViewModel applicationViewModel, ApplicationContantUtilities contantUtilities, IFileConverter fileConverter, FileUpload fileUpload, File file)
        {
            _contantUtilities = contantUtilities;
            _httpClientFactory = httpClientFactory;
            _apiContentProvider = apiContentProvider;
            _applicationViewModel = applicationViewModel;
            _fileConverter = fileConverter;
            _applicationViewModel.FileUpload = fileUpload;
            _applicationViewModel.FileUpload.File = file;
        }

        public async Task<TViewModel> PostRegisterAsync<TViewModel, TModel>(TModel model)
        {
            try
            {
                var content = _apiContentProvider.StringContent(model);

                var httpClient = _httpClientFactory.CreateClient("default");
                _response = await httpClient.PostAsync(_contantUtilities.RegisterEnpoint, content);

                _applicationViewModel = await ViewModelResponseAsync(_applicationViewModel, model as Applicant, _response);
            }
            catch (Exception ex)
            {
                _applicationViewModel = ViewModelResponse(_applicationViewModel, _response, ex.Message);
            }

            return (TViewModel)Convert.ChangeType(_applicationViewModel, typeof(TViewModel));
        }

        public async Task<TViewModel> PostUploadFileAsync<TViewModel, TModel>(TModel model)
        {
            try
            {
                var formFile = model as IFormFile;

                _applicationViewModel.FileUpload.File.Data = await _fileConverter.ToBase64String(formFile);
                _applicationViewModel.FileUpload.File.Mime = formFile.ContentType;

                var content = _apiContentProvider.StringContent(_applicationViewModel.FileUpload);
                var httpClient = _httpClientFactory.CreateClient("addHeaderApiKey");

                _response = await httpClient.PostAsync(_contantUtilities.UploadResumeEndpoint, content);

                _applicationViewModel = await ViewModelResponseAsync(_applicationViewModel, _response);
            }
            catch (Exception ex)
            {
                _applicationViewModel = ViewModelResponse(_applicationViewModel, _response, ex.Message);
            }

            return (TViewModel)Convert.ChangeType(_applicationViewModel, typeof(TViewModel));
        }

        public async Task<TViewModel> PostScheduleAsync<TViewModel, TModel>(TModel model, bool isToAutomate)
        {
            try
            {
                var schedule = model as Schedule;

                var httpClient = _httpClientFactory.CreateClient("addHeaderApiKey");
                _response = await BookingProcessAsync(httpClient, schedule, isToAutomate);

                _applicationViewModel = await ViewModelResponseAsync(_applicationViewModel, schedule, _response);
            }
            catch (Exception ex)
            {
                _applicationViewModel = ViewModelResponse(_applicationViewModel, _response, ex.Message);
            }

            return (TViewModel)Convert.ChangeType(_applicationViewModel, typeof(TViewModel));
        }

        public async Task<TViewModel> ViewModelResponseAsync<TViewModel, TModel>(TViewModel viewModelType, TModel model, HttpResponseMessage responseMessage)
        {
            var viewModel = viewModelType as ApplicationViewModel;

            if (responseMessage.IsSuccessStatusCode)
                viewModel.IsSuccess = true;
            else
                viewModel.IsSuccess = false;

            viewModel.Model = model;
            viewModel.Message = await responseMessage.Content.ReadAsStringAsync();
            viewModel.StatusCode = responseMessage.StatusCode;

            return (TViewModel)Convert.ChangeType(viewModel, typeof(TViewModel));
        }

        public async Task<TViewModel> ViewModelResponseAsync<TViewModel>(TViewModel viewModelType, HttpResponseMessage responseMessage)
        {
            using (responseMessage)
            {
                var viewModel = viewModelType as ApplicationViewModel;

                if (responseMessage.IsSuccessStatusCode)
                    viewModel.IsSuccess = true;
                else
                    viewModel.IsSuccess = false;

                viewModel.Message = await responseMessage.Content.ReadAsStringAsync();
                viewModel.StatusCode = responseMessage.StatusCode;

                return (TViewModel)Convert.ChangeType(viewModel, typeof(TViewModel));
            }
        }

        public TViewModel ViewModelResponse<TViewModel>(TViewModel viewModelType, HttpResponseMessage responseMessage, string exceptionMessage)
        {
            using (responseMessage)
            {
                var viewModel = viewModelType as ApplicationViewModel;

                if (responseMessage.IsSuccessStatusCode)
                    viewModel.IsSuccess = true;
                else
                    viewModel.IsSuccess = false;

                viewModel.Message = exceptionMessage;
                viewModel.StatusCode = responseMessage.StatusCode;

                return (TViewModel)Convert.ChangeType(viewModel, typeof(TViewModel));
            }
        }

        private async Task<HttpResponseMessage> BookingProcessAsync(HttpClient httpClient, Schedule schedule, bool isToAutomate)
        {
            var timeValue = TimeExtractor(schedule);
            var hours = timeValue.Item1;
            var minutes = timeValue.Item2;
            var meridiem = timeValue.Item3;
            var dateTimeToIncrement = Convert.ToDateTime($"{schedule.ProposedDate} {hours}:{minutes} {meridiem}");
            var response = new HttpResponseMessage();

            if (isToAutomate)
            {
                response = await AutomateProcessAsync(httpClient, schedule, dateTimeToIncrement);
            }
            else
            {
                var content = _apiContentProvider.StringContent(schedule);
                response = await httpClient.PostAsync(_contantUtilities.ScheduleEndpoint, content);
            }

            return response;
        }

        private Tuple<int, int, string> TimeExtractor(Schedule schedule)
        {
            var time = schedule.ProposedTime;
            var meridiemIndex = schedule.ProposedTime.IndexOf('M') - 1;
            var meridiem = time.Substring(meridiemIndex);

            var proposedTime = schedule.ProposedTime;
            var digitsOnly = string.Empty;
            var hours = string.Empty;
            var minutes = string.Empty;

            switch (proposedTime.Length)
            {
                case 6:
                    digitsOnly = proposedTime.Remove(4);
                    hours = digitsOnly.Remove(2);
                    minutes = digitsOnly.Remove(0, 2);
                    break;
                case 5:
                    digitsOnly = proposedTime.Remove(3);
                    hours = digitsOnly.Remove(1);
                    minutes = digitsOnly.Remove(0, 1);
                    break;
                case 4:
                    hours = proposedTime.Remove(2);
                    minutes = "0";
                    break;
                case 3:
                    hours = proposedTime.Remove(1);
                    minutes = "0";
                    break;
                default:
                    break;
            }

            return Tuple.Create(Convert.ToInt32(hours), Convert.ToInt32(minutes), meridiem);
        }

        private async Task<HttpResponseMessage> AutomateProcessAsync(HttpClient httpClient, Schedule schedule, DateTime dateTime)
        {
            var response = new HttpResponseMessage();

            do
            {
                schedule.ProposedDate = dateTime.ToString("yyyy-MM-dd");
                schedule.ProposedTime = (dateTime.Minute > 0) ? dateTime.ToString("hmmtt").ToUpper()
                                                              : dateTime.ToString("htt").ToUpper();

                var content = _apiContentProvider.StringContent(schedule);
                response = await httpClient.PostAsync(_contantUtilities.ScheduleEndpoint, content);

                dateTime = dateTime.AddMinutes(30);

            } while ((!response.IsSuccessStatusCode) && (response.StatusCode != HttpStatusCode.BadRequest));

            return response;
        }
    }
}
