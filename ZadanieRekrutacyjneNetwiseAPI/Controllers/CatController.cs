using Microsoft.AspNetCore.Mvc;
using ZadanieRekrutacyjneNetwiseAPI.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZadanieRekrutacyjneNetwiseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        private readonly ICatService _catFactService;
        private readonly IFileService _fileService;

        public CatController(ICatService catFactService, IFileService fileService)
        {
            _catFactService = catFactService;
            _fileService = fileService;
        }

        [HttpGet]
        [Route("SaveFactsFromApi")]
        public async Task<IActionResult> Get(int amount)
        {
            try
            {
                var factsList = await _catFactService.GetCatFactsAsync(amount);

                await _fileService.SaveFactsToFileAsync(factsList);

                return Ok(new { message = $"Pomyślnie zapisano {amount} faktów" });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllSavedCats")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var factsList = await _fileService.GetAllFactsFromFileAsync();

                if (factsList.Count == 0)
                {
                    return NotFound();
                }

                return Ok(factsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("GetSavedCatsPage")]
        public async Task<IActionResult> Get(int page, int pageSize)
        {
            try
            {
                var factsList = await _fileService.GetFactsPageAsync(page, pageSize);

                return Ok(factsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
