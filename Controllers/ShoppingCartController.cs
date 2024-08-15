using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Storee.Entities;
using Storee.Repository.Abstract;
using Storee.ViewModel.Create;
using Storee.ViewModel.Update;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storee.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShoppingCartController : ControllerBase
	{
		private readonly IShoppingCartService _shoppingCartService;
		private readonly ILogger<ShoppingCartController> _logger;

		public ShoppingCartController(IShoppingCartService shoppingCartService, ILogger<ShoppingCartController> logger)
		{
			_shoppingCartService = shoppingCartService;
			_logger = logger;
		}

		[HttpGet("{sessionId}")]
		public async Task<IActionResult> GetShoppingCart(string sessionId)
		{
			var cart = await _shoppingCartService.GetOrCreateShoppingCartAsync(sessionId);
			return Ok(cart);
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddCartItem([FromBody] AddCartItemRequest request)
		{
			var result = await _shoppingCartService.AddCartItemAsync(request.SessionId, request.ProductId, request.Quantity, request.UnitPrice);
			if (result)
			{
				return Ok(new { Message = "Item added to cart successfully" });
			}
			return BadRequest("Failed to add item to cart");
		}

		[HttpPut("update")]
		public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartItemRequest request)
		{
			var result = await _shoppingCartService.UpdateCartItemAsync(request.SessionId, request.CartItemId, request.Quantity);
			if (result)
			{
				return Ok(new { Message = "Cart item updated successfully" });
			}
			return BadRequest("Failed to update cart item");
		}

		[HttpDelete("remove/{sessionId}/{cartItemId}")]
		public async Task<IActionResult> RemoveCartItem(string sessionId, int cartItemId)
		{
			var result = await _shoppingCartService.RemoveCartItemAsync(sessionId, cartItemId);
			if (result)
			{
				return Ok(new { Message = "Cart item removed successfully" });
			}
			return BadRequest("Failed to remove cart item");
		}
	}
}
