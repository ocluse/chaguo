using Chaguo.Data;
using CsvHelper;
using System.Formats.Asn1;
using System.Globalization;

namespace Chaguo
{
    public class DataService
    {
        private readonly IHttpClientFactory _httpClient;
        public DataService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Constituency>> LoadDataAsync()
        {
            var client = _httpClient.CreateClient();
            HttpResponseMessage response = await client.GetAsync("https://liquidsnow.blob.core.windows.net/public-space/election2022/data.csv");
            response.EnsureSuccessStatusCode();
            using Stream dataStream = response.Content.ReadAsStream();
            using StreamReader reader = new(dataStream);
            using CsvReader cvs = new(reader, CultureInfo.InvariantCulture);

            var records = cvs.GetRecords<ConstituencyNullable>();

            List<Constituency> data = new();

            foreach (var record in records)
            {
                data.Add(new()
                {
                    Ruto = record.Ruto ?? 0,
                    Raila = record.Raila ?? 0,
                    Wajackoyah = record.Wajackoyah ?? 0,
                    Mwaure = record.Mwaure ?? 0,
                    Rejected = record.Rejected ?? 0,
                    Polled = record.Ruto != null
                });
            }
            
            return data;
        }

        private class ConstituencyNullable
        {
            public int? Ruto { get; set; }
            public int? Raila { get; set; }
            public int? Wajackoyah { get; set; }
            public int? Mwaure { get; set; }
            public int? Rejected { get; set; }
        }
    }
}
