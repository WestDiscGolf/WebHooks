using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.WebHooks
{
    /// <summary>
    /// 
    /// </summary>
    public class FitbitWebHookAttribute : WebHookAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public FitbitWebHookAttribute() : base("fitbit")
        {
        }
    }
}
