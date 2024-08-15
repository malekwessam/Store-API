using Storee.Entities;
using Storee.ViewModel.Create;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Abstract
{
	public interface IOrderService
	{
		Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
		Task<decimal> CreateOrderAsync(string customerEmail, List<OrderItemRequest> orderItemRequests, int countryId, decimal shippingCost);
		Task<Order> GetOrderByIdAsync(int orderId);
		Task<IEnumerable<Order>> GetAllOrdersAsync();
		Task CreateOrderAsync(Order order);
		Task UpdateOrderAsync(Order order);
		Task DeleteOrderAsync(int orderId);
		
	}
}
