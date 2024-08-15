using Storee.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Abstract
{
	public interface IShoppingCartService
	{
		Task<ShoppingCart> GetOrCreateShoppingCartAsync(string sessionId);
		Task<bool> AddCartItemAsync(string sessionId, int productId, int quantity, decimal unitPrice);
		Task<bool> UpdateCartItemAsync(string sessionId, int cartItemId, int quantity);
		Task<bool> RemoveCartItemAsync(string sessionId, int cartItemId);
	}
}
