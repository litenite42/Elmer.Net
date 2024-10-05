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
    public class SubmitButtonTagHelper(IReCaptchaApi _api) : TagHelper
    {
        public override void Init(TagHelperContext context)
        {
            base.Init(context);
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Attributes.SetAttribute("class", "btn btn-primary elmer-btn");
            output.Attributes.Add("data-verify-url", _api.VerifyUrl);
            output.Attributes.Add("data-site-key", _api.SiteKey);

            output.Content.Append("Submit");
            return base.ProcessAsync(context, output);
        }
    }
}
