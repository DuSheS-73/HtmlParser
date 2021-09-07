using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Net.Http;

namespace HTMLParser.Services
{
    public class ParserService
    {
        private readonly string _url;
        private readonly HttpClient _client;

        public ParserService(string url)
        {
            _url = url;
            _client = new HttpClient();
        }

        public async Task<string> ParseAsync()
        {
            var htmlString = await _client.GetStringAsync(_url);

            var html = new HtmlDocument();
            html.LoadHtml(htmlString);

            var dom = html.DocumentNode;

            var title = dom.QuerySelector("h1");
            var articleTags = dom.QuerySelectorAll("#article-body > p, #article-body > ul li");

            var sb = new StringBuilder(title.InnerText);
            foreach (var tag in articleTags)
            {
                sb.AppendLine(tag.InnerText);
            }

            return sb.ToString();
        }
    }
}
