using System;
using Microsoft.AspNetCore.WebHooks.Metadata;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    internal static class FitbitServiceCollectionSetup
    {
        public static void AddFitbitServices(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            WebHookMetadata.Register<FitbitMetadata>(services);

            // replace WebHookGetHeadRequestFilter.Order
        }
    }
}
