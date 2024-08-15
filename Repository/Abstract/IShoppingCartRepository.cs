using Storee.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Abstract
{
	public interface IShoppingCartRepository
	{

		Task<ShoppingCart> GetShoppingCartBySessionIdAsync(string sessionId);
		Task<ShoppingCart> CreateShoppingCartAsync(ShoppingCart shoppingCart);
		Task<bool> AddCartItemAsync(CartItem cartItem);
		Task<bool> UpdateCartItemAsync(CartItem cartItem);
		Task<bool> RemoveCartItemAsync(int cartItemId);
	}
}
