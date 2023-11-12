using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.DTOs.UserDTOs;
using PizzaApp.Services.UserServices.Interfaces;
using PizzaApp.Shared.CustomExceptions.UserExceptions;
using PizzaApp.Shared.Requests;

namespace PizzaApp.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : BaseController
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDTO user)
		{
			try
			{
				var request = new RegisterUserRequest
				{
					Email = user.Email,
					Username = user.Username,
					Password = user.Password
				};

				var response = await _userService.RegisterUserAsync(request);

				return Response(response);
			}
			catch (UserDataException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDTO user)
		{
			try
			{
				var request = new LoginUserRequest
				{
					Username = user.UserName,
					Password = user.Password
				};

				var response = await _userService.LoginUserAsync(request);

				return Response(response);
			}
			catch (UserDataException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var response = await _userService.GetAllUsers();

			if (response.IsSuccessfull)
				return Ok(response);

			return BadRequest(response.Errors);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserById(string id)
		{
			var response = await _userService.GetUserByIdAsync(id);

			if (response.IsSuccessfull)
				return Ok(response.Result);

			return NotFound(response.Errors);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDTO updatedUser)
		{
			var response = await _userService.UpdateUserAsync(id, updatedUser);

			if (response.IsSuccessfull)
				return Ok(response.Result);

			return BadRequest(response.Errors);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(string id)
		{
			var response = await _userService.DeleteUserAsync(id);

			if (response.IsSuccessfull)
				return Ok($"{nameof(DeleteUser)} was deleted");

			return BadRequest(response.Errors);
		}
	}
}
