using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace White.Knight.Tests.Domain
{
	public sealed class Order
	{
        [JsonPropertyName("id")]
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid OrderId { get; set; }

		[Required, ForeignKey("Customer")]
		public Guid CustomerId { get; set; }

		public string OrderKey { get; set; }

		public bool Active { get; set; }

		public DateTime OrderCreated { get; set; }

		public Customer Customer { get; set; }
	}
}