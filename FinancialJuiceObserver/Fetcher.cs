using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml;
using System.Xml.Linq;

namespace FinancialJuiceObserver
{
    internal class Fetcher
    {
        HttpClient httpClient = new HttpClient();

        public async Task<JsonNode> FetchFeed()
        {
            using var xmlStream = await httpClient.GetStreamAsync("https://www.financialjuice.com/feed.ashx?xy=rss");
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlStream);
            JsonNode json = JsonNode.Parse(JsonConvert.SerializeXmlNode(xDoc));
            return json;
        }
    }
}
