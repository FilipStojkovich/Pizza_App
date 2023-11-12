using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.DTOs.UserDTOs
{
	public class RegisterUserDTO
	{
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
	}
}
