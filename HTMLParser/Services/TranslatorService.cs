using HTMLParser.ConfigurationModels;
using HTMLParser.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HTMLParser.Services
{
    public class TranslatorService
    {
        private readonly string _route = $"/translate?api-version={AppSettings.TranslatorApiVersion}{{0}}";

        public TranslatorService(params string[] languagesTo)
        {
            var sb = new StringBuilder();


            if (languagesTo.Length == 0)
            {
                sb.Append("&to=ru");
            }

            foreach (var lang in languagesTo)
            {
                sb.Append($"&to={lang}");
            }

            _route = String.Format(_route, sb.ToString());
        }

        public async Task<string> TranslateAsync(string[] partsToTranslate)
        {
            var sb = new StringBuilder();

            foreach (var part in partsToTranslate)
            {
                object[] body = new object[] { new { Text = part } };
                var requestBody = JsonConvert.SerializeObject(body);

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(AppSettings.TranslatorApiEndpoint + _route);
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", AppSettings.TranslatorApiKey);
                    request.Headers.Add("Ocp-Apim-Subscription-Region", AppSettings.TranslatorApiRegion);

                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

                    var responseString = await response.Content.ReadAsStringAsync();
                    var tranlationsTexts = JsonConvert.DeserializeObject<List<TranslationResponse>>(responseString)
                                                        .SelectMany(x => x.Translations?.Select(xx => xx.Text)).ToArray();
                    sb.AppendLine(String.Join('\n', tranlationsTexts));
                }
            }
            
            return sb.ToString();
        }
    }
}
