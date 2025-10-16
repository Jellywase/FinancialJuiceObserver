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
            string xmlString = await httpClient.GetStringAsync("https://www.financialjuice.com/feed.ashx?xy=rss");
            if (string.IsNullOrEmpty(xmlString))
            {
                throw new Exception("Feed returns empty string");
            }
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlString);
            JsonNode json = JsonNode.Parse(JsonConvert.SerializeXmlNode(xDoc));
            return json;
        }
    }
}
