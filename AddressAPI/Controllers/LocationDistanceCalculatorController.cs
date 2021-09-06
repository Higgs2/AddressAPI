using AddressAPI.Entities;
using AddressAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AddressAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("distance")]
    public class LocationDistanceCalculatorController : ControllerBase
    {
        private readonly IDistanceCalculator _distanceCalculator;

        public LocationDistanceCalculatorController(IDistanceCalculator distanceCalculator)
        {
            _distanceCalculator = distanceCalculator;
        }

        /// <summary>
        /// Calculates distance between two cities.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CalculateLocationDistance(string from, string to)
        {
            var distance = _distanceCalculator.CalculateDistance(from, to);
            return distance != 0 ? Ok(new Response<float>(distance)
            {
                Data = distance,
                Succeeded = true
            }) : StatusCode(204, new Response<float>(distance)
            {
                Data = 0,
                Succeeded = false,
                Message = "Could not calculate distance between these two points." 
            });
        }
    }
}
