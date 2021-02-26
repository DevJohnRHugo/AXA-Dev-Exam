using Pointwest.Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pointwest.Test.Interfaces.Http
{
    public interface IApiContentProvider
    {
        //public StringContent StringContent(Applicant applicant);
        //public StringContent StringContent(Schedule schedule);
        public StringContent StringContent<TModel>(TModel model);
    }
}
