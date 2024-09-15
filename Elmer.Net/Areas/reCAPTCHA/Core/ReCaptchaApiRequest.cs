namespace Elmer.Net.Areas.reCAPTCHA.Core
{
    public class ReCaptchaApiRequest : IReCaptchaApiRequest
    {
        public ReCaptchaApiRequest() {

            Response = "";
            Secret = "";
        }
        public string Response { get; set; }
        public string Secret { get; set; }
        public string? RemoteIp { get; set; }
        public string ToString()
        {
            return $"Rsp:{Response},Sct:{Secret[0..6]}***,Rip:{RemoteIp}";
        }
    }
}
