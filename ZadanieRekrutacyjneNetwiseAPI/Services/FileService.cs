using Newtonsoft.Json;
using System.Text;
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
    }
}
