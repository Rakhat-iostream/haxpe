using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haxpe.Infrastructure
{
    public class CultureSwitcherModel
    {
        public string DefaultLanguage { get; set; }
        public List<string> SupportedLanguages { get; set; }

        public CultureSwitcherModel()
        {
            DefaultLanguage = "en";
            SupportedLanguages = new List<string>
            {
                "en",
                "de"
            };
        }
    }
}
