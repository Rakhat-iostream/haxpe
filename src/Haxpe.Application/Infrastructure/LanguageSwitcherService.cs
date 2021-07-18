using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure
{
    public class LanguageSwitcherService : ILanguageSwitcherService
    {

        public async Task<string> SetLanguage(string language)
        {
            var input = new CultureSwitcherModel();
            string res = null;

            CultureInfo lang = new CultureInfo(language);

            if (input.SupportedLanguages.Contains(lang.TwoLetterISOLanguageName))
            {
                res = language;
            }
            else
            {
                language = input.DefaultLanguage;
                res = language;
            }
            return res;
        }
    }
}