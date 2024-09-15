using Elmer.Net.Areas.reCAPTCHA.Core;
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
    [HtmlTargetElement(TagStructure = TagStructure.WithoutEndTag)]
    public class SubmitButtonTagHelper(IReCaptchaApi _api) : TagHelper
    {
        public override void Init(TagHelperContext context)
        {
            base.Init(context);
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";

            output.AddClass("btn btn-primary elmer-btn", htmlEncoder: HtmlEncoder.Default);
            output.Attributes.Add("data-verify-url", _api.VerifyUrl);
            output.Attributes.Add("data-site-key", _api.SiteKey);

            return base.ProcessAsync(context, output);
        }
    }
}
