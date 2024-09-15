using Elmer.Net.Areas.reCAPTCHA.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Elmer.Net.Areas.reCAPTCHA.Extensions
{
    /// <summary>
    /// Extends IServiceCollection to add reCAPTCHA necessary functionality
    /// </summary>
    public static class IServiceCollectionsExtensions
    {
        /// <summary>
        /// Register reCAPTCHA's configuration for DI
        /// </summary>
        /// <typeparam name="TGoogleApi">Implements <seealso cref="IReCaptchaApi"/> to provide configuration values for reCAPTCHA</typeparam>
        /// <param name="collection">App's Services Collection; typically provided in app's Startup or equivalent</param>
        /// <param name="configuration">App's Configuration</param>
        public static void AddReCaptcha<TGoogleApi>(this IServiceCollection collection, IConfiguration configuration) where TGoogleApi : class, IReCaptchaApi, new()
        {
            TGoogleApi api = new();
            var section = configuration.GetSection(ReCaptchaApi.recaptchaIndex);
            section?.Bind(api);

            collection.AddSingleton<IReCaptchaApi>(api);
        }
    }
}
