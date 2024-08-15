using Storee.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Abstract
{
	public interface IOrderRepository
	{
		Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
		
		Task<Order> GetOrderByIdAsync(int orderId);
		Task<IEnumerable<Order>> GetAllOrdersAsync();
		Task<Order> CreateOrderAsync(Order order);
		Task UpdateOrderAsync(Order order);
		Task DeleteOrderAsync(int orderId);
		
	}
}
