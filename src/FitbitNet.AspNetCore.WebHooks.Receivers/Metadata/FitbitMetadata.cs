using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.WebHooks.Metadata
{
    /// <summary>
    /// 
    /// </summary>
    public class FitbitMetadata : WebHookMetadata
    {
        /// <summary>
        /// 
        /// </summary>
        public FitbitMetadata() : base("fitbit")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override WebHookBodyType BodyType => WebHookBodyType.Json;
    }
}
