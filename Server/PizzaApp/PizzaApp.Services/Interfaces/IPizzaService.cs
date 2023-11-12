using PizzaApp.DTOs.PizzaDTOs;
using PizzaApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Services.Interfaces
{
	public interface IPizzaService
	{
		Task<Response<List<PizzaDTO>>> GetAllPizzas();
		Task<Response<PizzaDTO>> GetPizzaById(int id);
		Task<Response<PizzaDTO>> CreatePizza(string userId, AddPizzaDTO addPizzaDTO);
		Task<Response<PizzaDTO>> UpdatePizza(string userId, int pizzaId, UpdatePizzaDTO updatePizzaDTO);
		Task<Response> DeletePizza(string userId, int pizzaId); 
	}
}
