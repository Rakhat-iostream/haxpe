using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorLight;

namespace Haxpe.V1.Emails
{
    public class TemplateRenderer : ITemplateRenderer
    {
        private readonly RazorLightEngine engine;

        public TemplateRenderer()
        {
            engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(TemplateRenderer).Assembly)
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task<string> RenderAsync(string templateName, object model, string cultureName)
        {
            var culture = new CultureInfo(cultureName);
            var res = typeof(TemplateRenderer).Assembly.GetManifestResourceNames();
            var key = $"Haxpe.Emails.{templateName}.{culture.TwoLetterISOLanguageName}.cshtml";
            string result = await engine.CompileRenderAsync(key, model);
            return result;
        }
    }
}
