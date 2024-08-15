using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storee.Data;
using Storee.Entities;
using Storee.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Implement
{
	public class OrderItemRepository : IOrderItemRepository
	{
		private readonly StoreDbContext DbContext;
		private readonly ILogger<OrderItemRepository> _logger;
		public OrderItemRepository(StoreDbContext DbContext, ILogger<OrderItemRepository> logger)
		{
			this.DbContext = DbContext;
			_logger = logger;
		}

		public async Task<bool> UpdateOrderItemAsync(OrderItem orderItem)
		{
			DbContext.OrderItem.Update(orderItem); // تحديث عنصر الطلب في مجموعة OrderItems
			var result = await DbContext.SaveChangesAsync(); // حفظ التغييرات في قاعدة البيانات
			return result > 0; // إرجاع true إذا تم الحفظ بنجاح
		}
		public async Task<bool> AddOrderItemAsync(OrderItem orderItem)
		{
			try
			{
				DbContext.OrderItem.Add(orderItem);
				await DbContext.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error adding order item: {ex.Message}");
				return false;
			}
		}

		public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem)
		{
			DbContext.OrderItem.Add(orderItem);
			await DbContext.SaveChangesAsync();
			return orderItem;
		}

		public async Task<bool> DeleteOrderItemAsync(int id)
		{
			var categoryToRemove = await DbContext.OrderItem.FindAsync(id);
			DbContext.OrderItem.Remove(categoryToRemove);
			return await DbContext.SaveChangesAsync() > 0;
		}

		public Task<OrderItem> GetOrderItemAsync(int id)
		{
			return this.DbContext.OrderItem.FindAsync(id).AsTask();
		}

		public Task<List<OrderItem>> GetOrderItemsAsync()
		{
			return this.DbContext.OrderItem.ToListAsync();
		}
		public async Task<OrderItem> UpdateOrderAsync(OrderItem orderItem)
		{
			DbContext.OrderItem.Update(orderItem);
			await DbContext.SaveChangesAsync();
			return orderItem;
		}

		
	}
}
