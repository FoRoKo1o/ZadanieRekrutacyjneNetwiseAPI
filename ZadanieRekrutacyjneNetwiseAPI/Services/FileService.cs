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
                streamWriter.Close();
            }
        }
        public async Task<List<CatFact>> GetAllFactsFromFileAsync()
        {
            var factsList = new List<CatFact>();

            if(File.Exists(_filePath))
            {
               using (var streamReader = new StreamReader(_filePath))
                {
                   string line;
                   while ((line = await streamReader.ReadLineAsync()) != null)
                    {
                       var fact = JsonConvert.DeserializeObject<CatFact>(line);
                        if(fact != null)
                        {
                            factsList.Add(fact);
                        }
                   }
                    streamReader.Close();
                }
            }
            return factsList;
        }
    }
}
