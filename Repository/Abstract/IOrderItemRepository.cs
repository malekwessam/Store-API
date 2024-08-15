using Storee.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Abstract
{
	public interface IOrderItemRepository
	{
		Task<List<OrderItem>> GetOrderItemsAsync();
		Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem);
		Task<bool> AddOrderItemAsync(OrderItem orderItem);
		Task<bool> DeleteOrderItemAsync(int id);
		Task<OrderItem> UpdateOrderAsync(OrderItem orderItem);
		Task<OrderItem> GetOrderItemAsync(int id);
		Task<bool> UpdateOrderItemAsync(OrderItem orderItem);
	}
}
