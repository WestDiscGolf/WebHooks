using System;
using Microsoft.AspNetCore.WebHooks;

namespace FitbitCoreReceiver.Controllers
{
    public class FitbitController
    {
        [FitbitWebHook]
        public IAsyncResult FitbitSubscription(string id, Notification[] data)
        {
            return null;
        }

        [FitbitWebHook(Id="my_id")]
        public IAsyncResult FitbitSubscriptionId(Notification[] data)
        {
            return null;
        }
    }
}
