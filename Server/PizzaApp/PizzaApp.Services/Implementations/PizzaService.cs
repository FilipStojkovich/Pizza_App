using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Dml;
using NPOI.Util;
using PizzaApp.DataAccess.DbContext;
using PizzaApp.Domain.Entities;
using PizzaApp.DTOs.PizzaDTOs;
using PizzaApp.Services.Interfaces;
using PizzaApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Services.Implementations
{
	public class PizzaService : IPizzaService
	{
		private readonly PizzaAppDbContext _dbContext;
		private readonly IMapper _mapper;

        public PizzaService(PizzaAppDbContext dbcontext, IMapper mapper)
        {
			_dbContext = dbcontext;
			_mapper = mapper;
        }

        public async Task<Response<PizzaDTO>> CreatePizza(string userId, AddPizzaDTO addPizzaDTO)
		{
			var pizza = _mapper.Map<Pizza>(addPizzaDTO);
			pizza.UserId = addPizzaDTO.UserId;

			_dbContext.Add(pizza);
			await _dbContext.SaveChangesAsync();

			var pizzaDTOResult = _mapper.Map<PizzaDTO>(pizza);

			return new Response<PizzaDTO>(pizzaDTOResult);
		}

		public async Task<Response> DeletePizza(string userId, int pizzaId)
		{
			var pizza = await _dbContext.Pizzas.FindAsync(pizzaId);

			if (pizza == null)
				return new Response("Pizza not found.");

			if (pizza.UserId == userId)
				return new Response("You do not have permission to delete this pizza!");

			_dbContext.Pizzas.Remove(pizza);
			await _dbContext.SaveChangesAsync();

			return new Response() { IsSuccessfull = true };
		}

		public async Task<Response<List<PizzaDTO>>> GetAllPizzas()
		{
			var pizzas = await _dbContext.Pizzas.ToListAsync();

			var pizzaDTOs = _mapper.Map<List<PizzaDTO>>(pizzas);

			return new Response<List<PizzaDTO>>(pizzaDTOs);
		}

		public async Task<Response<PizzaDTO>> GetPizzaById(int id)
		{
			var pizza = await _dbContext.Pizzas.FindAsync(id);

			if (pizza == null)
				return new Response<PizzaDTO>("Pizza not found.");

			var pizzaDTOs = _mapper.Map<PizzaDTO>(pizza);

			return new Response<PizzaDTO>(pizzaDTOs);
		}

		public async Task<Response<PizzaDTO>> UpdatePizza(string userId, int pizzaId,  UpdatePizzaDTO updatePizzaDTO)
		{
			var pizza = await _dbContext.Pizzas.FindAsync(pizzaId);

			if (pizza == null)
				return new Response<PizzaDTO>("Pizza not found.");

			if (pizza.UserId != userId)
				return new Response<PizzaDTO>("You do not have permission to update this pizza!");

			_mapper.Map(pizza, updatePizzaDTO);
			await _dbContext.SaveChangesAsync();

			var pizzaDTOResult = _mapper.Map<PizzaDTO>(pizza);

			return new Response<PizzaDTO>(pizzaDTOResult);
		}
	}
}
