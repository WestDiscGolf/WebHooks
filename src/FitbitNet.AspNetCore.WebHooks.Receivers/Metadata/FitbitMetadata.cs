namespace Microsoft.AspNetCore.WebHooks.Metadata
{
    /// <summary>
    /// 
    /// </summary>
    public class FitbitMetadata : WebHookMetadata/*, IWebHookMetadata, IWebHookReceiver*/
    {
        /// <summary>
        /// 
        /// </summary>
        public FitbitMetadata()
            : base(FitbitConstants.ReceiverName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override WebHookBodyType BodyType => WebHookBodyType.Json;
    }
}
