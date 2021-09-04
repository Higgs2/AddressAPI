using AddressAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Linq;

namespace AddressAPI.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("address")]
    public class LocationController : ControllerBase
    {
        private readonly AddressDbContext _addressDbContext;

        public LocationController(AddressDbContext addressContext)
        {
            _addressDbContext = addressContext;
        }

        /// <summary>
        /// Retrieve an address. You may filter by address properties (e.g. country & city) and meta data (e.g. orderBy descending).
        /// </summary>
        /// <param name="address"></param>
        /// <param name="perPage"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromQuery] Address address, [FromQuery] string perPage = null, [FromQuery] string orderBy = null)
        {
            try
            {

                var addressResult = _addressDbContext.Addresses.Where(a => a.Equals(address));

                if (addressResult != null)
                    return Ok(addressResult);
                return NotFound();
            }
            catch (SqliteException exception)
            {
                return StatusCode(500);
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Address), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromQuery] Address address, [FromQuery] string perPage = null, [FromQuery] string orderBy = null)
        {
            try
            {
                _addressDbContext.Add(address);
                if (_addressDbContext.SaveChanges() != 0)
                    return Ok();
                return NotFound();
            }
            catch (SqliteException exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Address), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromQuery] Address address)
        {
            try
            {
                _addressDbContext.Update(address);
                if (_addressDbContext.SaveChanges() != 0)
                    return Ok();
                return NotFound();
            }
            catch (SqliteException exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromQuery] Address address)
        {
            try
            {
                _addressDbContext.Remove(address);
                if (_addressDbContext.SaveChanges() != 0)
                    return Ok();
                return NotFound();
            }
            catch (SqliteException exception)
            {
                return StatusCode(500);
            }
        }
    }
}
