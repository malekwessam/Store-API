using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Repository.Abstract;
using Store.Repository.Implement;
using Storee.Entities;
using Storee.Repository.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Implement
{
	public class OrderItemService : IOrderItemService
	{
		private readonly IOrderItemRepository orderItemRepository;
		
		public OrderItemService(IOrderItemRepository orderItemRepository)
		{
			this.orderItemRepository = orderItemRepository;
			
		}

		Task<bool> AddOrderItemAsync(OrderItem orderItem)
		{
			return orderItemRepository.AddOrderItemAsync(orderItem);

		}

		public Task<bool> UpdateOrderItemAsync(OrderItem orderItem)
		{
			return orderItemRepository.UpdateOrderItemAsync(orderItem);

		}

		public Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem)
		{
			return orderItemRepository.CreateOrderItemAsync(orderItem);
		}

		public async Task<bool> CreateOrderItemAsync(int orderId, int productId, int quantity, decimal unitPrice)
		{
			// قم بإنشاء عنصر الطلب
			var orderItem = new OrderItem
			{
				OrderId = orderId,
				ProductId = productId,
				Quantity = quantity,
				UnitPrice = unitPrice
			};

			// قم بإضافة عنصر الطلب إلى قاعدة البيانات
			return await orderItemRepository.AddOrderItemAsync(orderItem);
		}

		public Task<bool> DeleteOrderItemAsync(int id)
		{
			return orderItemRepository.DeleteOrderItemAsync(id);
		}

		public Task<OrderItem> GetOrderItemAsync(int id)
		{
			return orderItemRepository.GetOrderItemAsync(id);
		}

		public Task<List<OrderItem>> GetOrderItemsAsync()
		{
			return orderItemRepository.GetOrderItemsAsync();
		}

		


		public Task<OrderItem> UpdateOrderAsync(OrderItem ordeItem)
		{
			return orderItemRepository.UpdateOrderAsync(ordeItem);
		}

		public async Task<bool> UpdateOrderItemAsync(int orderItemId, int quantity, decimal unitPrice)
		{
			// جلب عنصر الطلب الحالي
			var existingOrderItem = await orderItemRepository.GetOrderItemAsync(orderItemId);
			if (existingOrderItem == null)
			{
				return false; // عنصر الطلب غير موجود
			}

			// تحديث خصائص عنصر الطلب
			existingOrderItem.Quantity = quantity;
			existingOrderItem.UnitPrice = unitPrice;

			// تحديث عنصر الطلب في قاعدة البيانات
			return await orderItemRepository.UpdateOrderItemAsync(existingOrderItem);
		}

		Task<bool> IOrderItemService.AddOrderItemAsync(OrderItem orderItem)
		{
			return orderItemRepository.AddOrderItemAsync(orderItem);
		}
	}
}
