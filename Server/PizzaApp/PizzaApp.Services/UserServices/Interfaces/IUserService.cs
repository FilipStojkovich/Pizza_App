using PizzaApp.DTOs.UserDTOs;
using PizzaApp.Shared.Requests;
using PizzaApp.Shared.Responses;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PizzaApp.Shared.Responses.Response;
using Response = PizzaApp.Shared.Responses.Response;

namespace PizzaApp.Services.UserServices.Interfaces
{
	public interface IUserService
	{
		Task<Response<RegisterUserResponse>> RegisterUserAsync(RegisterUserRequest request);
		Task<Response<LoginUserResponse>> LoginUserAsync(LoginUserRequest request);
		Task<Response> GetAllUsers();
		Task<Response<UserDTO>> GetUserByIdAsync(string id);
		Task<Response<UserDTO>> UpdateUserAsync(string id, UserDTO updateUser);
		Task<Response> DeleteUserAsync(string id);

	}
}
