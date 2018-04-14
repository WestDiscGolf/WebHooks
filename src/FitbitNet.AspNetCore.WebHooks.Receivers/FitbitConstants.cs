namespace FitbitNet.AspNetCore.WebHooks
{
    internal class FitbitConstants
    {
        public static string VerifyQueryParameterName => "verify";

        public static string ReceiverName => "fitbit";

        public static string SignatureHeaderName => "X-Fitbit-Signature";

        public static int OAuthClientMinLength => 32;

        public static int VerifyQueryCodeMinLength => 32;

        public static string OAuthClientSecretKey => "OAuthClientSecret";
    }
}
