using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Storee.Entities;
using Storee.Repository.Abstract;
using Storee.ViewModel.Create;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storee.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly ILogger<OrderController> _logger;

		public OrderController(IOrderService orderService, ILogger<OrderController> logger)
		{
			_orderService = orderService;
			_logger = logger;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrderById(int id)
		{
			var order = await _orderService.GetOrderByIdAsync(id);
			if (order == null)
			{
				return NotFound();
			}
			return Ok(order);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllOrders()
		{
			var orders = await _orderService.GetAllOrdersAsync();
			return Ok(orders);
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
		{
			try
			{
				// تحويل OrderItemRequest إلى OrderItem
				var orderItems = request.OrderItems.Select(item => new OrderItemRequest
				{
					ProductId = item.ProductId,
					Quantity = item.Quantity,
					UnitPrice = item.UnitPrice // هذا يمكن أن يتم تعديله بناءً على السعر الحالي للمنتج في البلد المحدد
				}).ToList();

				// إنشاء الطلب
				var totalAmount = await _orderService.CreateOrderAsync(request.CustomerEmail, orderItems, request.CountryId, request.ShippingCost);

				// تسجيل نجاح العملية
				_logger.LogInformation($"Order created successfully with total amount: {totalAmount}");

				return Ok(new { TotalAmount = totalAmount });
			}
			catch (Exception ex)
			{
				// تسجيل أي أخطاء تحدث
				_logger.LogError($"Error creating order: {ex.Message}");
				return StatusCode(500, "An error occurred while creating the order.");
			}
		}




		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateOrder(int id, [FromForm] Order order)
		{
			if (id != order.Id)
			{
				return BadRequest();
			}

			var existingOrder = await _orderService.GetOrderByIdAsync(id);
			if (existingOrder == null)
			{
				return NotFound();
			}

			await _orderService.UpdateOrderAsync(order);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			var existingOrder = await _orderService.GetOrderByIdAsync(id);
			if (existingOrder == null)
			{
				return NotFound();
			}

			await _orderService.DeleteOrderAsync(id);
			return NoContent();
		}
	}
}
