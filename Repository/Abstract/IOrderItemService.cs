using Storee.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Abstract
{
	public interface IOrderItemService
	{
		Task<List<OrderItem>> GetOrderItemsAsync();
		Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem);
		Task<bool> AddOrderItemAsync(OrderItem orderItem);
		Task<bool> CreateOrderItemAsync(int orderId, int productId, int quantity, decimal unitPrice);
		Task<bool> UpdateOrderItemAsync(int orderItemId, int quantity, decimal unitPrice);
		Task<bool> DeleteOrderItemAsync(int id);
		Task<OrderItem> UpdateOrderAsync(OrderItem orderItem);
		Task<OrderItem> GetOrderItemAsync(int id);
		Task<bool> UpdateOrderItemAsync(OrderItem orderItem);
	}
}
