using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.DTOs.OrderDTOs
{
	public class OrderDTO
	{
		public int Id { get; set; }
		public string UserId { get; set; } = string.Empty;
		public string AdressTo { get; set; } = string.Empty;
		public string? Description { get; set; }
		public int OrderPrice { get; set; }
	}
}
