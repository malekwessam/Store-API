using Microsoft.Extensions.Logging;
using Store.Repository.Abstract;
using Store.Repository.Implement;
using Storee.Entities;
using Storee.Repository.Abstract;
using Storee.ViewModel.Create;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Implement
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderItemRepository _orderItemRepository;
		private readonly IPPriceRepository _productPriceRepository;
		private readonly IPaymentRepository _paymentRepository;
		private readonly IProductRepository _productRepository;
		private readonly ILogger<OrderService> _logger;
		public OrderService(IProductRepository productRepository,IOrderRepository orderRepository,IPPriceRepository pPriceRepository, IOrderItemRepository orderItemRepository, IPaymentRepository paymentRepository, ILogger<OrderService> logger)
		{
			this._orderRepository = orderRepository;
			this._orderItemRepository = orderItemRepository;
			this._productPriceRepository = pPriceRepository;
			this._paymentRepository = paymentRepository;
			this._productRepository = productRepository;
			_logger = logger;
		}

		

		public async Task<Order> GetOrderByIdAsync(int orderId)
		{
			return await _orderRepository.GetOrderByIdAsync(orderId);
		}

		public async Task<IEnumerable<Order>> GetAllOrdersAsync()
		{
			return await _orderRepository.GetAllOrdersAsync();
		}

		

		public async Task UpdateOrderAsync(Order order)
		{
			await _orderRepository.UpdateOrderAsync(order);
		}

		public async Task DeleteOrderAsync(int orderId)
		{
			await _orderRepository.DeleteOrderAsync(orderId);
		}

		public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
		{
			return await _orderRepository.GetOrderItemsByOrderIdAsync(orderId);
		}

		public Task CreateOrderAsync(Order order)
		{
			return _orderRepository.CreateOrderAsync(order);
		}

		public async Task<decimal> CreateOrderAsync(string customerEmail, List<OrderItemRequest> orderItemRequests, int countryId, decimal shippingCost)
		{
			var orderItems = new List<OrderItem>();
			decimal totalAmount = 0;

			foreach (var itemRequest in orderItemRequests)
			{
				var productPrice = await _productPriceRepository.GetProductPricessAsync(itemRequest.ProductId, countryId);
				if (productPrice != null)
				{
					var unitPrice = productPrice.Price;

					// جلب المنتج للتأكد من وجود خصم
					var product = await _productRepository.GetProductAsync(itemRequest.ProductId);
					if (product != null && product.IsSale.HasValue && product.IsSale.Value)
					{
						var salePercentage = product.SalePercentage ?? 0; // استخدام قيمة افتراضية 0 إذا كانت SalePercentage null
						unitPrice -= unitPrice * (salePercentage / 100);
					}

					var orderItem = new OrderItem
					{
						ProductId = itemRequest.ProductId,
						Quantity = itemRequest.Quantity,
						UnitPrice = unitPrice
					};
					orderItems.Add(orderItem);

					totalAmount += orderItem.Quantity * unitPrice; // حساب المبلغ الإجمالي هنا مباشرةً
				}
			}

			totalAmount += shippingCost; // إضافة تكلفة الشحن للمبلغ الإجمالي

			var order = new Order
			{
				CustomerEmail = customerEmail,
				TotalAmount = totalAmount,
				ShippingCost = shippingCost,
			};

			var createdOrder = await _orderRepository.CreateOrderAsync(order);

			foreach (var orderItem in orderItems)
			{
				orderItem.OrderId = createdOrder.Id;
				await _orderItemRepository.AddOrderItemAsync(orderItem);
			}

			var payment = new Payment
			{
				OrderId = createdOrder.Id,
				Amount = totalAmount,
				PaymentStatus = "Pending",
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			await _paymentRepository.CreatePaymentAsync(payment);

			return totalAmount;
		}



	}
}
