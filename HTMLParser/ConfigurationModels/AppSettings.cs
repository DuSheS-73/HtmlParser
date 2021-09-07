using System.Configuration;

namespace HTMLParser.ConfigurationModels
{
    internal static class AppSettings
    {
        internal static string TranslatorApiKey => ConfigurationManager.AppSettings["translatorApiKey"];
        internal static string TranslatorApiEndpoint => ConfigurationManager.AppSettings["translatorApiEndpoint"];
        internal static string TranslatorApiVersion => ConfigurationManager.AppSettings["translatorApiVersion"];
        internal static string TranslatorApiRegion => ConfigurationManager.AppSettings["translatorApiRegion"];
        internal static string ResultDirectoryPath => ConfigurationManager.AppSettings["resultDirectoryPath"];
    }
}
