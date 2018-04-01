using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebHooks;
using Microsoft.AspNetCore.WebHooks.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace FitbitNet.AspNetCore.WebHooks.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class FitbitVerifySubscriberFilter : WebHookSecurityFilter, IResourceFilter, IWebHookReceiver // need this!
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="hostingEnvironment"></param>
        /// <param name="loggerFactory"></param>
        public FitbitVerifySubscriberFilter(
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment,
            ILoggerFactory loggerFactory)
            : base(configuration, hostingEnvironment, loggerFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Check that it is a Get request
            var routeData = context.RouteData;
            var request = context.HttpContext.Request;
            if (routeData.TryGetWebHookReceiverName(out var receiverName)
                && HttpMethods.IsGet(request.Method))
            {
                var result = EnsureValidCode(context.HttpContext.Request, routeData, receiverName);
                if (result != null)
                {
                    context.Result = result;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="routeData"></param>
        /// <param name="receiverName"></param>
        /// <returns></returns>
        private IActionResult EnsureValidCode(HttpRequest request, RouteData routeData, string receiverName)
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
                    $"'{FitbitConstants.VerifyQueryParameterName}' query " +
                    "parameter.",
                    receiverName);

                return new NotFoundResult();
            }

            var secretKey = GetSecretKey(
                receiverName,
                routeData,
                WebHookConstants.CodeParameterMinLength,
                WebHookConstants.CodeParameterMaxLength);
            if (secretKey == null)
            {
                // if no secret code then it shouldn't expose the end point to a potential attacker
                return new NotFoundResult();
            }

            if (SecretEqual(code, secretKey))
            {
                // Good verify code return a 204
                return new StatusCodeResult((int)HttpStatusCode.NoContent);
            }

            // return 404 if bad verify code
            return new NotFoundResult();
        }
        
        /// <inheritdoc />
        public string ReceiverName => FitbitConstants.ReceiverName;

        /// <inheritdoc />
        public bool IsApplicable(string receiverName)
        {
            if (receiverName == null)
            {
                throw new ArgumentNullException(nameof(receiverName));
            }

            if (ReceiverName == null)
            {
                return true;
            }

            return string.Equals(ReceiverName, receiverName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
