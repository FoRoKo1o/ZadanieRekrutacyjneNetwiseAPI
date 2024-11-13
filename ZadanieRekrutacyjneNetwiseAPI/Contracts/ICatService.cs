using Microsoft.AspNetCore.Mvc;
using ZadanieRekrutacyjneNetwiseAPI.Data;

namespace ZadanieRekrutacyjneNetwiseAPI.Contracts
{
    public interface ICatService
    {
        Task<List<CatFact>> GetCatFactsAsync(int amount);
    }
}
