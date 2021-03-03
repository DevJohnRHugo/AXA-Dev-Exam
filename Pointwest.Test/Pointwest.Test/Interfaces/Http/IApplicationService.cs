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
        Task<TViewModel> PostRegisterAsync<TViewModel, TModel>(TModel model);
        Task<TViewModel> PostScheduleAsync<TViewModel, TModel>(TModel model, bool isToAutomate);
        Task<TViewModel> PostUploadFileAsync<TViewModel, TModel>(TModel model);
        Task<TViewModel> ViewModelResponseAsync<TViewModel, TModel>(TViewModel viewModel, TModel model, HttpResponseMessage responseMessage);

        Task<TViewModel> ViewModelResponseAsync<TViewModel>(TViewModel viewModel, HttpResponseMessage responseMessage);

        TViewModel ViewModelResponse<TViewModel>(TViewModel viewModel, HttpResponseMessage responseMessage, string exceptionMessage);
    }
}
