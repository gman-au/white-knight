using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace White.Knight.Tests.Domain
{
    public sealed class Customer
    {
        [JsonPropertyName("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CustomerId { get; set; }

        [JsonPropertyName("my_other_guid")] public Guid OtherGuid { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAlias { get; set; }

        public int CustomerNumber { get; set; }


        public DateTime CustomerCreated { get; set; }

        public string CustomerPrivateDetail { get; set; }

        public int CustomerType { get; set; }

        [NotMapped]
        [JsonPropertyName("not_what_you_expected")]
        public Order FavouriteOrder { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}