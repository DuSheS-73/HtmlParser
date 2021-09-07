using HTMLParser.Models;
using System.Collections.Generic;

namespace HTMLParser.Responses
{
    public class TranslationResponse
    {
        public DetectedLanguage DetectedLanguage { get; set; }
        public List<Translation> Translations { get; set; }
    }
}
