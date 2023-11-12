using PizzaApp.Domain.Entities;
using PizzaApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.DTOs.PizzaDTOs
{
	public class PizzaDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int Price { get; set; }
		public string UserId { get; set; } = string.Empty;
		public List<IngredientEnum> Ingredients { get; set; } = new List<IngredientEnum>();
		public Order? Order { get; set; }
	}
}
