using System;
using FitbitNet.AspNetCore.WebHooks;
using FitbitNet.AspNetCore.WebHooks.Filters;
using FitbitNet.AspNetCore.WebHooks.Metadata;
using Microsoft.AspNetCore.WebHooks.Metadata;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class FitbitServiceCollectionSetup
    {
        public static void AddFitbitServices(IServiceCollection services, Action<FitbitWebhookReceiverOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // register the options action
            services.Configure(setupAction);

            //services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, MvcOptionsSetup>());
            WebHookMetadata.Register<FitbitMetadata>(services);

            services.TryAddSingleton<FitbitVerifySubscriberFilter>();
            services.TryAddSingleton<FitbitVerifySignatureFilter>();
        }

        //private class MvcOptionsSetup : IConfigureOptions<MvcOptions>
        //{
        //    /// <inheritdoc />
        //    public void Configure(MvcOptions options)
        //    {
        //        if (options == null)
        //        {
        //            throw new ArgumentNullException(nameof(options));
        //        }

        //        options.Filters.AddService<FitbitVerifySubscriberFilter>(WebHookVerifyCodeFilter.Order);
        //        options.Filters.AddService<FitbitVerifySignatureFilter>(WebHookSecurityFilter.Order);
        //    }
        //}
    }
}
