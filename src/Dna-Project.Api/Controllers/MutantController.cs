namespace Dna_Project.Api.Controllers
{
    using Core.Dto;
    using Core.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/mutant")]
    [ApiController]
    public class MutantController : ControllerBase
    {
        readonly IMutantService _mutantService;

        public MutantController(IMutantService mutantService)
        {
            _mutantService = mutantService;
        }

        /// <summary>
        /// Mutant endpoint
        /// </summary>
        /// <param name="dna">dna</param>
        /// <returns>Is mutant or not</returns>
        /// <response code="200">Successful</response>    
        /// <response code="403">Forbidden</response>    
        [HttpPost]
        public async Task<ActionResult> MutantAsync(DnaDto dna)
        {
            if (await _mutantService.IsMutantAsync(dna.Dna))
                return Ok(true);
            else
                return StatusCode(403);
        }

        /// <summary>
        /// Stats endpoint
        /// </summary>
        /// <returns>Stats</returns>
        /// <response code="200">Successful</response>    
        [HttpGet]
        [Route("stats")]
        public async Task<ActionResult> GetStats()
        {
            return Ok(await _mutantService.GetCountDnaAsync());
        }
    }
}
