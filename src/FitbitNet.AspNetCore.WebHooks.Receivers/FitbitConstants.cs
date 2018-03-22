namespace Microsoft.AspNetCore.WebHooks
{
    class FitbitConstants
    {
        public static string VerifyQueryParameterName => "verify";

        public static string ReceiverName => "fitbit";

        public static string SignatureHeaderName => "X-Fitbit-Signature";

        public static int MinLength => 32;

        public static int MaxLength => 32;

        public static string OAuthClientSecretKey => "OAuthClientSecret";
    }
}
