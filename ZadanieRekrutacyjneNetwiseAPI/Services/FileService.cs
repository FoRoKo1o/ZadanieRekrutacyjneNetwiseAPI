using Newtonsoft.Json;
using ZadanieRekrutacyjneNetwiseAPI.Contracts;
using ZadanieRekrutacyjneNetwiseAPI.Data;

namespace ZadanieRekrutacyjneNetwiseAPI.Services
{
    public class FileService : IFileService
    {
        private readonly string _filePath;

        public FileService()
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "facts.txt");
        }
        public async Task SaveFactsToFileAsync(List<CatFact> facts)
        {
            using (var streamWriter = new StreamWriter(_filePath, append: true))
            {
                foreach (var fact in facts)
                {
                    var jsonContent = JsonConvert.SerializeObject(fact);

                    await streamWriter.WriteLineAsync(jsonContent);
                }
            }
        }
        public async Task<List<CatFact>> GetAllFactsFromFileAsync()
        {
            var factsList = new List<CatFact>();

            if (File.Exists(_filePath))
            {
                using (var streamReader = new StreamReader(_filePath))
                {
                    string line;
                    while ((line = await streamReader.ReadLineAsync()) != null)
                    {
                        var fact = JsonConvert.DeserializeObject<CatFact>(line);
                        if (fact != null)
                        {
                            factsList.Add(fact);
                        }
                    }
                }
            }
            return factsList;
        }
        public async Task<int> GetFileSizeAsync()
        {
            int lineCount = 0;
            if (File.Exists(_filePath))
            {
                await foreach (var _line in File.ReadLinesAsync(_filePath))
                {
                    lineCount++;
                }
            }
            return lineCount;
        }

        public async Task<List<CatFact>> GetFactsPageAsync(int page, int pageSize)
        {
            var facts = new List<CatFact>();

            if (!File.Exists(_filePath))
            {
                return facts;
            }

            int totalLines = await GetFileSizeAsync();
            int startLine = (page - 1) * pageSize;

            if (startLine >= totalLines)
            {
                return facts;
            }

            using (var streamReader = new StreamReader(_filePath))
            {
                for (int i = 0; i < startLine; i++)
                {
                    await streamReader.ReadLineAsync();
                }

                int lineCount = 0;
                while (lineCount < pageSize && !streamReader.EndOfStream)
                {
                    var line = await streamReader.ReadLineAsync();
                    var fact = JsonConvert.DeserializeObject<CatFact>(line);

                    if (fact != null)
                    {
                        facts.Add(fact);
                    }

                    lineCount++;
                }
            }

            return facts;
        }
    }
}
