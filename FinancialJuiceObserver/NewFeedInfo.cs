using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialJuiceObserver
{
    public class NewFeedInfo
    {
        public DateTime pubDate { get; private set; }
        public string title { get; private set; } = string.Empty;
        public string description { get; private set; } = string.Empty;

        public NewFeedInfo(DateTime pubDate, string title, string description)
        {
            this.pubDate = pubDate;
            this.title = title;
            this.description = description;
        }
    }
}
