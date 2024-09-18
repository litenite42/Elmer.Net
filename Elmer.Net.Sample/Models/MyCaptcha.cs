namespace Elmer.Net.Sample.Models
{
    public class MyCaptcha : Core.IReCaptchaApi
    {
        public string SiteKey { get; set ; }
        public string SecretKey { get ; set ; }
        public string VerifyUrl { get; set; } = "/_api/Verify";
    }
}
