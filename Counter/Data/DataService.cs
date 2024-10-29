using Counter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Counter.Data
{
    public class DataService
    {
        private const string FileName = "counters.json";

        public async Task<List<CounterModel>> LoadCountersAsync()
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, FileName);

            if (!File.Exists(path))
                return new List<CounterModel>();

            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<List<CounterModel>>(json);
        }

        public async Task SaveCountersAsync(List<CounterModel> counters)
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, FileName);

            var json = JsonSerializer.Serialize(counters);
            await File.WriteAllTextAsync(path, json);
        }
    }
}
