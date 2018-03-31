using System;
using System.ComponentModel;
using Microsoft.AspNetCore.WebHooks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class FitbitMvcBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddFitbitWebHooks(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return AddFitbitWebHooks(builder, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IMvcBuilder AddFitbitWebHooks(this IMvcBuilder builder, Action<FitbitWebhookReceiverOptions> setupAction)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            FitbitServiceCollectionSetup.AddFitbitServices(builder.Services, setupAction);

            return builder.AddWebHooks();
        }
    }
}
