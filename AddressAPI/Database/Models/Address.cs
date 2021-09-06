#nullable enable
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressAPI
{
    /// <summary>
    /// Represents an address record.
    /// </summary>
    [Table("Address")]
    public class Address
    {
        [Key]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }


        public string? Street { get; set; }

        public string? HouseNumbering { get; set; }

        public string? PostalCode { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

    }
}
