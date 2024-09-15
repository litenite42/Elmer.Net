namespace Elmer.Net.Areas.reCAPTCHA.Core
{
    public class ReCaptchaApi : IReCaptchaApi
    {
        /// <summary>
        /// Key value used to reference the stored configurations for the api
        /// </summary>
        public const string recaptchaIndex = "Google:reCAPTCHA";
        public ReCaptchaApi()
        {
            SiteKey = "";
            SecretKey = "";
            VerifyUrl = "/reCAPTCHA/_api/Verify";
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string SiteKey { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// <para>Defaults to /reCAPTCHA/_api/Verify</para>
        /// </summary>
        public string VerifyUrl { get; set; }
    }
}
