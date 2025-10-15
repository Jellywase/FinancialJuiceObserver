public class Program
{
    public static async Task Main(string[] args)
    {
        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", "E");
        var response = await httpClient.GetStringAsync("https://www.financialjuice.com/feed.ashx?xy=rss");
    }
}