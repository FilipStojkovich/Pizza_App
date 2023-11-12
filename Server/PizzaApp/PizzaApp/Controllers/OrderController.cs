using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Domain.Entities;
using PizzaApp.DTOs.OrderDTOs;
using PizzaApp.Services.Interfaces;
using PizzaApp.Shared.CustomExceptions.OrderExceptions;
using PizzaApp.Shared.CustomExceptions.ServerExceptions;
using System.Security.Claims;

namespace PizzaApp.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : BaseController
	{
		private readonly IOrderService _orderService;
		private readonly UserManager<User> _userManager;

		public OrderController(IOrderService orderService, UserManager<User> userManager)
		{
			_orderService = orderService;
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllOrders()
		{
			try
			{
				var response = await _orderService.GetAllOrders();

				return Response(response);
			}
			catch (OrderDataException e)
			{
				return BadRequest(e.Message);
			}
			catch (InternalServerErrorException e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrderById(int id)
		{
			try
			{
				var response = await _orderService.GetOrderById(id);

				return Response(response);
			}
			catch (OrderDataException e)
			{
				return BadRequest(e.Message);
			}
			catch (InternalServerErrorException e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder([FromBody] AddOrderDTO addOrderDTO)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

				var response = await _orderService.CreateOrder(userId, addOrderDTO);

				return Response(response);
			}
			catch (OrderDataException e)
			{
				return BadRequest(e.Message);
			}
			catch (InternalServerErrorException e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDTO updateOrderDTO)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

				var response = await _orderService.UpdateOrder(userId, id, updateOrderDTO);

				return Response(response);
			}
			catch (OrderDataException e)
			{
				return BadRequest(e.Message);
			}
			catch (InternalServerErrorException e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			try
			{
				var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

				var response = await _orderService.DeleteOrder(userId, id);

				return Response(response);
			}
			catch (OrderDataException e)
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
