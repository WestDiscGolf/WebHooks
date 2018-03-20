using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebHooks;
using Newtonsoft.Json.Linq;

namespace FitbitCoreReceiver.Controllers
{
    public class FitbitController
    {
        [FitbitWebHook]
        public IAsyncResult FitbitScription(string id, JObject data)
        {
            return null;
        }
    }
}
