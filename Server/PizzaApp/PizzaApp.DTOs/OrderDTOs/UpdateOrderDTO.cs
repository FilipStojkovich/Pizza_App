using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PizzaApp.DTOs.OrderDTOs
{
	public class UpdateOrderDTO
	{
		[JsonIgnore]
		public int Id { get; set; }
		[JsonIgnore]
		public string UserId { get; set; } = string.Empty;
		public string AdressTo { get; set; } = string.Empty;
		public string? Description { get; set; }
	}
}
