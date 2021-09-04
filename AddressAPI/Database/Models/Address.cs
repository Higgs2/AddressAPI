using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressAPI
{
    [Table("Address")]
    public class Address
    {
        [Key]
        public int Id { get; set;  }

        public string? Street { get; set; }

        public string? HouseNumbering { get; set; }

        public string? PostalCode { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

 
    }
}
