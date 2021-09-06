using AddressAPI.Entities;
using AddressAPI.Entities.Pagination;
using AddressAPI.Models;
using AddressAPI.Services.Interfaces;
using AddressAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AddressAPI.Controllers
{
    /// <summary>
    /// Endpoint to search addresses by their property (e.g. city) and run various editing operations on it.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [Route("addresses")]
    public class LocationController : ControllerBase
    {
        private readonly AddressDbContext _addressDbContext;
        private readonly IPaginationURICreator _paginationURICreator;

        public LocationController(AddressDbContext addressContext, IPaginationURICreator paginationCreator)
        {
            _addressDbContext = addressContext;
            _paginationURICreator = paginationCreator;
        }

        /// <summary>
        /// Retrieve an address. You may filter by address properties (e.g. country and city) and meta data (e.g. orderBy descending).
        /// </summary>
        /// <param name="address"></param>
        /// <param name="perPage"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([FromQuery] Address address, [FromQuery] PaginationFilter paginationOptions, [FromQuery] string orderBy = null, [FromQuery] string orderByColumnName = null)
        {
            try
            {
                var validFilter = new PaginationFilter(paginationOptions.PageNumber, paginationOptions.PageSize);
                var route = Request.Path.Value;

                var addressQuery = _addressDbContext.Addresses.Where(a => a.Id == address.Id ||
                a.Street == address.Street ||
                a.HouseNumbering == address.HouseNumbering ||
                a.PostalCode == address.PostalCode ||
                a.City == address.City ||
                a.Country == address.Country);

                if (orderBy != null)
                {
                    var addressesFiltered = addressQuery.Skip((paginationOptions.PageNumber - 1) * paginationOptions.PageSize)
.Take(paginationOptions.PageSize)
.AsQueryable().OrderBy($"{orderByColumnName} {orderBy}").ToList();

                    if (addressesFiltered.Count() != 0)
                    {
                        var pagedReponse = PaginationUtilities.CreatePagedReponse<Address>(addressesFiltered, validFilter, addressesFiltered.Count(), _paginationURICreator, route, address);
                        return Ok(pagedReponse);
                    }
                }

                return NotFound(new Response<Address>()
                {
                    Succeeded = false,
                    Message = "Could not find any entries with specified filters."
                });
            }
            catch (DbUpdateException exception)
            {
                Console.WriteLine(exception);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates a new address entry.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Address), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromQuery] Address address)
        {
            try
            {
                _addressDbContext.Add(address);
                if (_addressDbContext.SaveChanges() != 0)
                    return Ok();
                return NotFound();
            }
            catch (DbUpdateException exception)
            {
                Console.WriteLine(exception);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update an address entry using it's id.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Address), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromQuery] Address address)
        {
            try
            {
                var addressQuery = _addressDbContext.Addresses.Find(address.Id);
                addressQuery.Street = address.Street;
                addressQuery.PostalCode = address.PostalCode;
                addressQuery.HouseNumbering = address.HouseNumbering;
                addressQuery.City = address.City;
                addressQuery.Country = address.Country;

                if (_addressDbContext.SaveChanges() != 0)
                    return Ok();
                return NotFound();
            }
            catch (DbUpdateException exception)
            {
                Console.WriteLine(exception);
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Delete an address entry using it's id.
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromQuery] int addressId)
        {
            try
            {
                var addressQuery = _addressDbContext.Addresses.Find(addressId);
                _addressDbContext.Remove(addressQuery);
                if (_addressDbContext.SaveChanges() != 0)
                    return Ok();
                return NotFound();
            }
            catch (DbUpdateException exception)
            {
                Console.WriteLine(exception);
                return StatusCode(500);
            }
        }

    }
}
