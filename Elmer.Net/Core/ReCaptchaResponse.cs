using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Elmer.Net.Core
{
    public class ReCaptchaResponse
    {
        [JsonPropertyName("action")]
        public string Action { get; set; }
        [JsonPropertyName("score")]
        public double Score { get;set; }
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("challenge_ts")]
        public DateTime ChallengeTS { get; set; }
        [JsonPropertyName("hostname")]
        public string HostName { get; set; }
        [JsonPropertyName("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
