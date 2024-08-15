using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Store.Repository.Abstract;
using Store.Repository.Implement;
using Storee.Entities;
using Storee.Repository.Abstract;
using Storee.ViewModel.Create;
using Storee.ViewModel.Get;
using Storee.ViewModel.Update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storee.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderItemController : ControllerBase
	{
		private readonly IOrderItemService _orderItemService;

		public OrderItemController(IOrderItemService orderItemService)
		{
			_orderItemService = orderItemService;
		}
		[HttpGet("all", Name = "GetOrderItems")]
		[ProducesResponseType(typeof(List<OrderItemViewModel>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetOrderItemsAsync()
		{

			var wishlists = await _orderItemService.GetOrderItemsAsync();

			var wishlistsViewModel = wishlists.Select(s => new OrderItemViewModel()
			{
				Id = s.Id,
				OrderId = Convert.ToInt32(s.OrderId),
				ProductId = Convert.ToInt32(s.ProductId),
				Quantity = s.Quantity,
				UnitPrice = s.UnitPrice
			}).ToList();

			return Ok(wishlistsViewModel);
		}

		[HttpGet("{id}", Name = "GetOrderItem")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(OrderItemViewModel), StatusCodes.Status200OK)]
		public async Task<ActionResult> Get(int id)
		{

			var category = await _orderItemService.GetOrderItemAsync(id);
			if (category == null)
				return NotFound();


			var model = new OrderItemViewModel()
			{
				Id = category.Id,
				ProductId = category.ProductId == null ? null : category.ProductId,
				OrderId = category.OrderId == null ? null : category.OrderId,

				UnitPrice = category.UnitPrice,
				Quantity = category.Quantity,


			};
			return Ok(model);
		}
		[HttpPost]
		public async Task<IActionResult> CreateOrderItem([FromForm] CreateOrderItem request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _orderItemService.CreateOrderItemAsync(request.OrderId, request.ProductId, request.Quantity, request.UnitPrice);
			if (result)
			{
				return Ok(new { Message = "Order item created successfully" });
			}
			return BadRequest("Failed to create order item");
		}

		[HttpPut("{id}", Name = "UpdateOrderItem")]

		public async Task<IActionResult> UpdateOrderItem([FromForm] UpdateOrderItem request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _orderItemService.UpdateOrderItemAsync(request.OrderItemId, request.Quantity, request.UnitPrice);
			if (result)
			{
				return Ok(new { Message = "Order item updated successfully" });
			}
			return BadRequest("Failed to update order item");
		}




		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Delete(int id)
		{
			var product = await _orderItemService.GetOrderItemAsync(id);
			if (product == null)
				return NotFound();

			// System.IO.File.Delete(product.PathImage);
			var isSuccess = await _orderItemService.DeleteOrderItemAsync(id);
			return Ok();
		}
	}
}
