namespace Elmer.Net.Core
{
    /// <summary>
    /// Defines the properties necessary to process reCAPTCHA requests server-side
    /// </summary>
    public interface IReCaptchaApi
    {
        /// <summary>
        /// your reCAPTCHA api instance's public site key
        /// </summary>
        string SiteKey { get; set; }
        /// <summary>
        /// your reCAPTCHA api instance's private key
        /// </summary>
        string SecretKey { get; set; }
        /// <summary>
        /// Where to send requests to be authenticated with your site key and secret key combination
        /// </summary>
        string VerifyUrl { get; set; }
    }
}