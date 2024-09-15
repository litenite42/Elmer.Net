using Elmer.Net.Areas.reCAPTCHA.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Elmer.Net.Areas.reCAPTCHA.Controllers
{
    public class _apiController(IReCaptchaApi recaptchaApi, ILogger<_apiController> logger) : Controller
    {
        private readonly string _googleUrl = "https://www.google.com/recaptcha/api/siteverify";
        public async Task<IActionResult> Verify(IReCaptchaApiRequest request)
        {
            if (request == null)
            {
                return BadRequest("Must receive recaptcha client-side information before proceeding.");
            }

            if (string.IsNullOrEmpty(request.Secret))
            {
                logger.LogError("No Site key configured for this request");
                return BadRequest("API configuration error.");
            }

            if (string.IsNullOrEmpty(request.Response))
            {
                logger.LogError("Missing Request's Response Token");
                return BadRequest("Invalid token configuration.");
            }

            using HttpClient client = new();
            client.BaseAddress = new(_googleUrl);
            client.DefaultRequestHeaders
                .Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string requestContent = "";
            try
            {
                requestContent = JsonSerializer.Serialize(request);
            }
            catch (NotSupportedException)
            {
                logger.LogError("An Error Occurred trying to serialize the request: {Request}", request.ToString());
                return BadRequest("An error occurred trying to send request to API.");
            }

            HttpRequestMessage requestMessage = new(HttpMethod.Post, _googleUrl)
            {
                Content = new StringContent(requestContent, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await client.SendAsync(requestMessage);
            return Json(response.Content);
        }
    }
}
