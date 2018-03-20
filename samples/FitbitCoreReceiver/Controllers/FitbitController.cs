using System;
using Microsoft.AspNetCore.WebHooks;
using Newtonsoft.Json.Linq;

namespace FitbitCoreReceiver.Controllers
{
    public class FitbitController
    {
        [FitbitWebHook]
        public IAsyncResult FitbitSubscription(string id, JArray data)
        {
            return null;
        }

        [FitbitWebHook(Id="my_id")]
        public IAsyncResult FitbitSubscriptionId(JArray data)
        {
            return null;
        }
    }
}
