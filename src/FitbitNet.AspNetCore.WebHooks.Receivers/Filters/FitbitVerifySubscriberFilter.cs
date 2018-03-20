using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebHooks.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Microsoft.AspNetCore.WebHooks.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class FitbitVerifySubscriberFilter : WebHookVerifyCodeFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="hostingEnvironment"></param>
        /// <param name="verifyCodeMetadata"></param>
        /// <param name="loggerFactory"></param>
        public FitbitVerifySubscriberFilter(
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment,
            IEnumerable<IWebHookVerifyCodeMetadata> verifyCodeMetadata,
            ILoggerFactory loggerFactory)
            : base(configuration, hostingEnvironment, verifyCodeMetadata, loggerFactory)
        {
        }

        /// <inheritdoc />
        protected override IActionResult EnsureValidCode(HttpRequest request, RouteData routeData, string receiverName)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (routeData == null)
            {
                throw new ArgumentNullException(nameof(routeData));
            }
            if (receiverName == null)
            {
                throw new ArgumentNullException(nameof(receiverName));
            }

            var result = EnsureSecureConnection(receiverName, request);
            if (result != null)
            {
                return result;
            }

            var code = request.Query[FitbitConstants.VerifyQueryParameterName];
            if (StringValues.IsNullOrEmpty(code))
            {
                Logger.LogWarning(
                    400,
                    "A '{ReceiverName}' WebHook verification request must contain a " +
                    $"'{WebHookConstants.CodeQueryParameterName}' query " +
                    "parameter.",
                    receiverName);

                // TODO: FIX

                var message = string.Format("It gone wrong! {0}, {1}, {2}",
                    CultureInfo.CurrentCulture,
                    receiverName,
                    WebHookConstants.CodeQueryParameterName);
                var noCode = new BadRequestObjectResult(message);

                return noCode;
            }

            var secretKey = GetSecretKey(
                receiverName,
                routeData,
                WebHookConstants.CodeParameterMinLength,
                WebHookConstants.CodeParameterMaxLength);
            if (secretKey == null)
            {
                return new NotFoundResult();
            }

            if (!SecretEqual(code, secretKey))
            {
                //Logger.LogWarning(
                //    401,
                //    $"The '{WebHookConstants.CodeQueryParameterName}' query parameter provided in the HTTP request " +
                //    "did not match the expected value.");

                //var message = string.Format("It also gone wrong {0}, {1}",
                //    CultureInfo.CurrentCulture,
                //    WebHookConstants.CodeQueryParameterName);
                //var invalidCode = new BadRequestObjectResult(message);

                //return invalidCode;
                return new NotFoundResult();
            }

            return new NoContentResult();
        }
    }
}
