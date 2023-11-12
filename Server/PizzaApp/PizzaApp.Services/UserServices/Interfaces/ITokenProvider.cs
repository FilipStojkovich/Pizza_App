using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Services.UserServices.Interfaces
{
	public interface ITokenProvider<T>
	{
		Task<T?> GetTokenAsync(string key);
		Task SetTokenValue(string key, T value);
	}
}
