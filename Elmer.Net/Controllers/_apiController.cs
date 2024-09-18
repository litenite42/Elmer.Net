using Elmer.Net.Core;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Elmer.Net.Controllers
{
    public class _apiController(IReCaptchaApi recaptchaApi, ILogger<_apiController> logger, IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly string _googleUrl = "https://www.google.com";

        [HttpPost]
        public async Task<IActionResult> Verify([FromBody] ReCaptchaApiRequest request)
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

            var client = httpClientFactory.CreateClient();
            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={recaptchaApi.SecretKey}&response={request.Response}");
            var recaptchaResult = JsonSerializer.Deserialize<ReCaptchaResponse>(response);

            if (recaptchaResult == null)
            {
                return Problem("An error occurred trying to deserialize response from Google.", statusCode: 500);
            }

            return Json(recaptchaResult);
        }
    }
}
