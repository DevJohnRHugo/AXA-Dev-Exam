using Microsoft.AspNetCore.Http;
using Pointwest.Test.Model;
using Pointwest.Test.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pointwest.Test.Interfaces.Http
{
    public interface IApplicationService
    {
        public Task<TViewModel> PostRegisterAsync<TViewModel, TModel>(TModel model);
        public Task<TViewModel> PostScheduleAsync<TViewModel, TModel>(TModel model);
        public Task<TViewModel> PostUploadFileAsync<TViewModel, TModel>(TModel model);
        public Task<TViewModel> ViewModelResponseAsync<TViewModel, TModel>(TViewModel viewModel, TModel model, HttpResponseMessage responseMessage);

        public Task<TViewModel> ViewModelResponseAsync<TViewModel>(TViewModel viewModel, HttpResponseMessage responseMessage);

        public TViewModel ViewModelResponse<TViewModel>(TViewModel viewModel, HttpResponseMessage responseMessage, string exceptionMessage);
    }
}
