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
        StringContent StringContent<TModel>(TModel model);
    }
}
