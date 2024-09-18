using Elmer.Net.Core;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Elmer.Net.CustomTagHelpers
{
    public class IncludeRecaptchaTagHelper(IReCaptchaApi _api) : TagHelper
    {
        public override void Init(TagHelperContext context)
        {
            base.Init(context);
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "script";

            output.Attributes.Add("src", $"https://www.google.com/recaptcha/api.js?render={_api.SiteKey}");

            return base.ProcessAsync(context, output);
        }
    }
}
