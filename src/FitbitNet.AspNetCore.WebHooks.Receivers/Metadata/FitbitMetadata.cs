using Microsoft.AspNetCore.WebHooks.Metadata;

namespace FitbitNet.AspNetCore.WebHooks.Metadata
{
    /// <summary>
    /// Class which defines the FitbitMetadata for the FitbitWebhook attribute. This specifies the type of body type the requests
    /// are expecting as well as defining the receiver name.
    /// </summary>
    public class FitbitMetadata : WebHookMetadata
    {
        /// <summary>
        /// Basic constructor defining that the receiver name is <see cref="FitbitConstants.ReceiverName"/>
        /// </summary>
        public FitbitMetadata()
            : base(FitbitConstants.ReceiverName)
        {
        }

        /// <summary>
        /// Fitbit webhook only supports the Json body type
        /// </summary>
        public override WebHookBodyType BodyType => WebHookBodyType.Json;
    }
}
