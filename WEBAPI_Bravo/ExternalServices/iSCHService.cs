using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_Bravo.DinamicModel;

namespace WEBAPI_Bravo.ExternalServices
{
    public interface ISCHService
    {
        Task PostSCHDataAsync();


    }
}
