using AutoMapper;
using PizzaApp.DataAccess.Repositories.Interfaces;
using PizzaApp.Domain.Entities;
using PizzaApp.DTOs.OrderDTOs;
using PizzaApp.Services.Interfaces;
using PizzaApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Services.Implementations
{
	public class OrderService : IOrderService
	{
		private readonly IMapper _mapper;
		private readonly IOrderRepository _repository;

        public OrderService(IMapper mapper, IOrderRepository repository)
        {
			_mapper = mapper;
			_repository = repository;
        }

        public async Task<Response<OrderDTO>> CreateOrder(string userId, AddOrderDTO addOrderDTO)
		{
			var order = _mapper.Map<Order>(addOrderDTO);
			order.UserId = userId;

			await _repository.Add(order);
			await _repository.SaveChanges();

			var orderDTOResult = _mapper.Map<OrderDTO>(order);

			return new Response<OrderDTO>(orderDTOResult);
		}

		public async Task<Response> DeleteOrder(string userId, int orderId)
		{
			var order = await _repository.GetByIdInt(orderId);

			if (order == null)
				return new Response("Order not found.");

			if(order.UserId != userId)
				return new Response("You do not have permission to delete this order.");

			await _repository.Remove(order);
			await _repository.SaveChanges();

			return new Response();
		}

		public async Task<Response<List<OrderDTO>>> GetAllOrders()
		{
			var orders = await _repository.GetAll();
			var orderDTOs = _mapper.Map<List<OrderDTO>>(orders);

			return new Response<List<OrderDTO>>(orderDTOs);
		}

		public async Task<Response<OrderDTO>> GetOrderById(int id)
		{
			var order = await _repository.GetByIdInt(id);

			if (order == null)
				return new Response<OrderDTO>("Order not found.");

			var orderDTO = _mapper.Map<OrderDTO>(order);

			return new Response<OrderDTO>(orderDTO);
		}

		public async Task<Response<OrderDTO>> UpdateOrder(string userId, int orderId, UpdateOrderDTO updatedOrderDTO)
		{
			var order = await _repository.GetByIdInt(orderId);

			if (order == null)
				return new Response<OrderDTO>("Order not found.");

			if (order.UserId != userId)
				return new Response<OrderDTO>("You do not have permission to update this order.");

			var updatedOrder = _mapper.Map(updatedOrderDTO, order);
			updatedOrder.UserId = userId;
			updatedOrder.Id = order.Id;

			await _repository.SaveChanges();

			var orderDTOResult = _mapper.Map<OrderDTO>(order);

			return new Response<OrderDTO>(orderDTOResult);
		}
	}
}