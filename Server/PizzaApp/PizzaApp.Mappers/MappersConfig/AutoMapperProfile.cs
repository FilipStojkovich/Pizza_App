using AutoMapper;
using PizzaApp.Domain.Entities;
using PizzaApp.DTOs.OrderDTOs;
using PizzaApp.DTOs.PizzaDTOs;
using PizzaApp.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Mappers.MappersConfig
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			//user mapping
			CreateMap<User, LoginUserDTO>().ReverseMap();
			CreateMap<User, RegisterUserDTO>().ReverseMap();
			CreateMap<User, UserDTO>().ReverseMap();

			//pizza mapping
			CreateMap<Pizza, PizzaDTO>().ReverseMap();
			CreateMap<Pizza, AddPizzaDTO>().ReverseMap();
			CreateMap<Pizza, UpdatePizzaDTO>().ReverseMap();

			//order mapping
			CreateMap<Order, OrderDTO>().ReverseMap();
			CreateMap<Order, AddOrderDTO>().ReverseMap();
			CreateMap<Order, UpdateOrderDTO>().ReverseMap();
		}
	}
}
