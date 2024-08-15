using Microsoft.EntityFrameworkCore;
using Storee.Data;
using Storee.Entities;
using Storee.Repository.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storee.Repository.Implement
{
	public class OrderRepository : IOrderRepository
	{
		private readonly StoreDbContext DbContext;
		public OrderRepository(StoreDbContext DbContext)
		{
			this.DbContext = DbContext;
		}
		public async Task<Order> GetOrderByIdAsync(int orderId)
		{
			return await DbContext.Order.FindAsync(orderId);
		}

		public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
		{
			return await DbContext.OrderItem
				.Where(oi => oi.OrderId == orderId)
				.ToListAsync();
		}


		public async Task<Order> CreateOrderAsync(Order order)
		{
			DbContext.Order.Add(order);
			await DbContext.SaveChangesAsync();
			return order;
		}

		public async Task UpdateOrderAsync(Order order)
		{
			DbContext.Order.Update(order);
			await DbContext.SaveChangesAsync();
		}

		public async Task DeleteOrderAsync(int orderId)
		{
			var order = await DbContext.Order.FindAsync(orderId);
			if (order != null)
			{
				DbContext.Order.Remove(order);
				await DbContext.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Order>> GetAllOrdersAsync()
		{
			return await DbContext.Order.ToListAsync();
		}

	
	}
}