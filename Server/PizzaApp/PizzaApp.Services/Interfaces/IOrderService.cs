using PizzaApp.DTOs.OrderDTOs;
using PizzaApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Services.Interfaces
{
	public interface IOrderService
	{
		Task<Response<List<OrderDTO>>> GetAllOrders();
		Task<Response<OrderDTO>> GetOrderById(int id);
		Task<Response<OrderDTO>> CreateOrder(string userId, AddOrderDTO addOrderDTO);
		Task<Response<OrderDTO>> UpdateOrder(string userId, int orderId, UpdateOrderDTO updatedOrderDTO);
		Task<Response> DeleteOrder(string userId, int orderId);
	}
}
