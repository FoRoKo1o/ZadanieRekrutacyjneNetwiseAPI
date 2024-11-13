using ZadanieRekrutacyjneNetwiseAPI.Data;

namespace ZadanieRekrutacyjneNetwiseAPI.Contracts
{
    public interface IFileService
    {
        Task SaveFactsToFileAsync(List<CatFact> facts);

    }
}
