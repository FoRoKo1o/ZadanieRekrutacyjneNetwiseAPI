﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZadanieRekrutacyjneNetwiseAPI.Contracts;
using ZadanieRekrutacyjneNetwiseAPI.Data;
using ZadanieRekrutacyjneNetwiseAPI.Services;

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
    }
}
