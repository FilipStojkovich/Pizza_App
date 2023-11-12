using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaApp.DataAccess.DbContext;
using PizzaApp.DataAccess.Repositories.Implementations;
using PizzaApp.DataAccess.Repositories.Interfaces;
using PizzaApp.Services.Implementations;
using PizzaApp.Services.Interfaces;
using PizzaApp.Services.UserServices.Implementations;
using PizzaApp.Services.UserServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Helpers.DependencyInjectionHelper
{
	public static class DependencyInjectionHelper
	{
		public static void InjectDbContext(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<PizzaAppDbContext>(x => x.UseNpgsql(connectionString));
		}

		public static void InjectRepositories(IServiceCollection services)
		{
			services.AddTransient<IPizzaRepository, PizzaRepository>();
			services.AddTransient<IOrderRepository, OrderRepository>();
		}

		public static void InjectServices(IServiceCollection services)
		{
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<ITokenService, TokenService>();
			services.AddTransient<IPizzaService, PizzaService>();
			services.AddTransient<IOrderService, OrderService>();
		}
	}
}
