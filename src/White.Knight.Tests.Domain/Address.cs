using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace White.Knight.Tests.Domain
{
    public class Address
    {
        [JsonPropertyName("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AddressId { get; set; }

        [Required] [ForeignKey("Customer")] public Guid CustomerId { get; set; }

        [Required] [ForeignKey("Country")] public Guid CountryId { get; set; }

        public string AddressLine { get; set; }

        public Customer Customer { get; set; }

        public Country Country { get; set; }
    }
}