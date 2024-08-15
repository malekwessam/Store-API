using Storee.Entities;
using Storee.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storee.Repository.Implement
{
	public class ShoppingCartService: IShoppingCartService
	{
		private readonly IShoppingCartRepository _repository;

		public ShoppingCartService(IShoppingCartRepository repository)
		{
			_repository = repository;
		}

		public async Task<ShoppingCart> GetOrCreateShoppingCartAsync(string sessionId)
		{
			var cart = await _repository.GetShoppingCartBySessionIdAsync(sessionId);
			if (cart == null)
			{
				cart = new ShoppingCart
				{
					SessionId = sessionId,
					CreatedAt = DateTime.UtcNow,
					
					CartItem = new List<CartItem>()
				};
				cart = await _repository.CreateShoppingCartAsync(cart);
			}
			return cart;
		}

		public async Task<bool> AddCartItemAsync(string sessionId, int productId, int quantity, decimal unitPrice)
		{
			var cart = await GetOrCreateShoppingCartAsync(sessionId);
			var cartItem = cart.CartItem.FirstOrDefault(ci => ci.ProductId == productId);
			if (cartItem == null)
			{
				cartItem = new CartItem
				{
					ShoppingCartId = cart.Id,
					ProductId = productId,
					Quantity = quantity,
					UnitPrice = unitPrice,
					TotalPrice = quantity * unitPrice
				};
				await _repository.AddCartItemAsync(cartItem);
			}
			else
			{
				cartItem.Quantity += quantity;
				cartItem.TotalPrice = cartItem.Quantity * unitPrice;
				await _repository.UpdateCartItemAsync(cartItem);
			}
			return true;
		}

		public async Task<bool> UpdateCartItemAsync(string sessionId, int cartItemId, int quantity)
		{
			var cart = await _repository.GetShoppingCartBySessionIdAsync(sessionId);
			var cartItem = cart?.CartItem.FirstOrDefault(ci => ci.Id == cartItemId);
			if (cartItem != null)
			{
				cartItem.Quantity = quantity;
				cartItem.TotalPrice = cartItem.Quantity * cartItem.UnitPrice;
				await _repository.UpdateCartItemAsync(cartItem);
				return true;
			}
			return false;
		}

		public async Task<bool> RemoveCartItemAsync(string sessionId, int cartItemId)
		{
			return await _repository.RemoveCartItemAsync(cartItemId);
		}
	}
}
