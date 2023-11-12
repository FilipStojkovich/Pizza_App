using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Domain.Entities;
using PizzaApp.DTOs.UserDTOs;
using PizzaApp.Services.UserServices.Interfaces;
using PizzaApp.Shared.CustomExceptions.UserExceptions;
using PizzaApp.Shared.Requests;
using PizzaApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Services.UserServices.Implementations
{
	public class UserService : IUserService
	{
		private readonly ITokenService _tokenService;
		private readonly UserManager<User> _userManager;

		public UserService(ITokenService tokenService, UserManager<User> userManager)
		{
			_tokenService = tokenService;
			_userManager = userManager;
		}


		public async Task<Response> DeleteUserAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user == null)
				return new Response("User not found");

			var result = await _userManager.DeleteAsync(user);

			if (!result.Succeeded)
				return new Response(result.Errors.Select(x => x.Description));

			return new Response();
		}

		public async Task<Response> GetAllUsers()
		{
			var response = new Response<List<UserDTO>>();
			var users = await _userManager.Users.ToListAsync();
			var userDTOs = users.Select(user => new UserDTO
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email
			}).ToList();

			response.Result = userDTOs;
			response.IsSuccessfull = true;

			return response;
		}

		public async Task<Response<UserDTO>> GetUserByIdAsync(string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user == null)
				return new Response<UserDTO>("User not found.");

			var userDTO = new UserDTO
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email
			};

			return new Response<UserDTO>(userDTO);
		}

		public async Task<Response<LoginUserResponse>> LoginUserAsync(LoginUserRequest request)
		{
			if (string.IsNullOrWhiteSpace(request?.Username))
				throw new UserDataException("Username is a required field!");

			if (string.IsNullOrWhiteSpace(request?.Password))
				throw new UserDataException("Password is a required field!");

			var user = await _userManager.FindByNameAsync(request.Username);

			if (user == null)
				return new("User doesn't exist.");

			var passwordIsValid = await _userManager.CheckPasswordAsync(user, request.Password);

			if (!passwordIsValid)
				return new("Passowrd is not valid!");

			var token = await _tokenService.GenerateTokenAsync(user);


			try
			{
				var t = new JwtSecurityTokenHandler().WriteToken(token);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			

			return new Response<LoginUserResponse>(new LoginUserResponse
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				ValidTo = token.ValidTo
			});
		}

		public async Task<Response<RegisterUserResponse>> RegisterUserAsync(RegisterUserRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.Username))
				throw new UserDataException("Username is a required field and must not be empty.");

			if (string.IsNullOrWhiteSpace(request.Email))
				throw new UserDataException("Email is a required field!");

			if (string.IsNullOrWhiteSpace(request.Password))
				throw new UserDataException("Password is a required field!");

			var user = new User
			{
				UserName = request.Username,
				Email = request.Email
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			if (!result.Succeeded)
				return new(result.Errors.Select(x => x.Description));

			return new(new RegisterUserResponse
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email
			});
		}

		public async Task<Response<UserDTO>> UpdateUserAsync(string id, UserDTO updatedUser)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user == null)
				return new Response<UserDTO>("User not found.");

			user.UserName = updatedUser.UserName;
			user.Email = updatedUser.Email;

			var result = await _userManager.UpdateAsync(user);

			if (!result.Succeeded)
				return new Response<UserDTO>(result.Errors.Select(x => x.Description));

			return new Response<UserDTO>(updatedUser);
		}
	}
}
