using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Domain.Entities;
using PizzaApp.DTOs.PizzaDTOs;
using PizzaApp.Services.Interfaces;
using PizzaApp.Shared.CustomExceptions.PizzaExceptions;
using PizzaApp.Shared.CustomExceptions.ServerExceptions;
using PizzaApp.Shared.Responses;
using System.Security.Claims;

namespace PizzaApp.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class PizzaController : BaseController
	{
		private readonly IPizzaService _pizzaService;
		private readonly UserManager<User> _userManager;

		public PizzaController(IPizzaService pizzaService, UserManager<User> userManager)
		{
			_pizzaService = pizzaService;
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllPizzas()
		{
			try
			{
				var response = await _pizzaService.GetAllPizzas();

				return Response(response);
			}
			catch (PizzaDataException e)
			{
				return BadRequest(e.Message);
			}
			catch(InternalServerErrorException e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPizzaById(int id)
		{
			try
			{
				var response = await _pizzaService.GetPizzaById(id);

				return Response(response);
			}
			catch (PizzaNotFoundException e)
			{
				return BadRequest(e.Message);
			}
			catch(InternalServerErrorException e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreatePizza([FromBody] AddPizzaDTO addPizzaDTO)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var response = await _pizzaService.CreatePizza(userId, addPizzaDTO);

				return Response(response);	
			}
			catch (PizzaDataException e)
			{
				return BadRequest(e.Message);
			}
			catch (InternalServerErrorException e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePizza(int id, [FromBody] UpdatePizzaDTO updatePizzaDTO)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var response = await _pizzaService.UpdatePizza(userId, id, updatePizzaDTO);

				return Response(response);
			}
			catch (PizzaDataException e)
			{
				return BadRequest(e.Message);
			}
			catch (InternalServerErrorException e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePizza(int id)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var response = await _pizzaService.DeletePizza(userId, id);

				return Response(response);
			}
			catch (PizzaDataException e)
			{
				return BadRequest(e.Message);
			}
			catch(InternalServerErrorException e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}
	}
}
