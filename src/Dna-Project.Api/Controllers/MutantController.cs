namespace Dna_Project.Api.Controllers
{
    using Core.Dto;
    using Core.Interfaces;
    using Microsoft.AspNetCore.Mvc;

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
        public ActionResult Mutant(DnaDto dna)
        {
            if (_mutantService.IsMutant(dna.Dna))
                return Ok(true);
            else
                return StatusCode(403);
        }
    }
}
