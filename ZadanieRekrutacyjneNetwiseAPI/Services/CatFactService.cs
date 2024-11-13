using Newtonsoft.Json;
using ZadanieRekrutacyjneNetwiseAPI.Contracts;
using ZadanieRekrutacyjneNetwiseAPI.Data;

namespace ZadanieRekrutacyjneNetwiseAPI.Services
{
    public class CatFactService : ICatService
    {
        private readonly HttpClient _httpClient;
        private readonly string _externalApiUrl = "https://catfact.ninja/fact";

        public CatFactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CatFact>> GetCatFactsAsync(int amount)
        {
            var factsList = new List<CatFact>();

            for (int i = 0; i < amount; i++)
            {
                var response = await _httpClient.GetAsync(_externalApiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var fact = JsonConvert.DeserializeObject<CatFact>(content);
                    if(fact != null)
                    {
                        fact.DownloadDate = DateTime.Now;
                        factsList.Add(fact);
                    }
                }
                else
                {
                    throw new HttpRequestException(response.StatusCode.ToString());
                }
            }

            return factsList;
        }
    }
}
