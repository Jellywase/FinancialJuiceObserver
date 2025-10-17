using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialJuiceObserver
{
    public class FeedWatcher
    {
        Fetcher fetcher;
        State state;
        const int newFeedDepth = 5;

        public FeedWatcher()
        {
            fetcher = new();
            string stateDirectory = Path.Combine(AppContext.BaseDirectory, "FeedState");
            Directory.CreateDirectory(stateDirectory);
            state = new(stateDirectory);
        }

        // 
        public async Task<IEnumerable<NewFeedInfo>> Update()
        {
            var json = await fetcher.FetchFeed();

            var feeds = json["rss"]["channel"]["item"].AsArray();

            // 새로운 피드 인덱스를 탐색 제한 깊이까지 탐색
            List<NewFeedInfo> newFeedInfos = new();
            for (int i = 0; i < feeds.Count; i++)
            {
                var feed = feeds[i];
                DateTime feedDate = DateTime.Parse(feed["pubDate"]?.ToString());
                if (feedDate > state.lastFeedDate)
                {
                    string title = feed["title"]?.ToString();
                    string description = feed["description"]?.ToString();
                    newFeedInfos.Add(new NewFeedInfo(feedDate, title, description));
                }
                if (newFeedInfos.Count >= newFeedDepth)
                { break; }
            }

            // 새로운 피드 시간 저장
            if (newFeedInfos.Count > 0)
            {
                state.UpdateLastFeedData(newFeedInfos[0].pubDate);
            }

            // 새로운 피드정보를 날짜 오름차순으로 변환 후 반환 (최신이 마지막에 오도록)
            newFeedInfos.Reverse();
            return newFeedInfos;
        }
    }
}
