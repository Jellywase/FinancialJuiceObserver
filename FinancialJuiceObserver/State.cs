using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static FinancialJuiceObserver.State;

namespace FinancialJuiceObserver
{
    
    internal class State
    {
        readonly object lockObj = new();
        readonly string directory;
        readonly string fileName = "FeedState";
        string path => Path.Combine(directory, fileName + ".json");
        public DateTime lastFeedDate => localData.lastFeedDate;
        StateData localData;

        public State(string directory)
        {
            this.directory = directory;
            Load();
        }

        public void UpdateLastFeedData(DateTime feedDate)
        {
            lock (lockObj)
            {
                localData.lastFeedDate = feedDate;
                Save();
            }
        }

        void Load()
        {
            if (!File.Exists(path))
            {
                localData = new StateData();
                Save();
                return;
            }
            var json = File.ReadAllText(path);
            localData = JsonSerializer.Deserialize<StateData>(json) ?? new StateData();
        }
        void Save()
        {
            lock (lockObj)
            {
                var json = JsonSerializer.Serialize(localData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
        }

        public class StateData
        {
            [JsonInclude]
            public DateTime lastFeedDate;
        }
    }
}
