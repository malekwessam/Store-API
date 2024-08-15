using Microsoft.EntityFrameworkCore;
using Storee.Data;
using Storee.Entities;
using Storee.Repository.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storee.Repository.Implement
{
	public class ShoppingCartRepository: IShoppingCartRepository
	{
		private readonly StoreDbContext DbContext;
		public ShoppingCartRepository(StoreDbContext DbContext)
		{
			this.DbContext = DbContext;
		}
		public async Task<ShoppingCart> GetShoppingCartBySessionIdAsync(string sessionId)
		{
			return await DbContext.ShoppingCart
				.Include(sc => sc.CartItem)
				.FirstOrDefaultAsync(sc => sc.SessionId == sessionId);
		}

		public async Task<ShoppingCart> CreateShoppingCartAsync(ShoppingCart shoppingCart)
		{
			DbContext.ShoppingCart.Add(shoppingCart);
			await DbContext.SaveChangesAsync();
			return shoppingCart;
		}

		public async Task<bool> AddCartItemAsync(CartItem cartItem)
		{
			DbContext.CartItem.Add(cartItem);
			await DbContext.SaveChangesAsync();
			return true;
		}

		public async Task<bool> UpdateCartItemAsync(CartItem cartItem)
		{
			DbContext.CartItem.Update(cartItem);
			await DbContext.SaveChangesAsync();
			return true;
		}

		public async Task<bool> RemoveCartItemAsync(int cartItemId)
		{
			var cartItem = await DbContext.CartItem.FindAsync(cartItemId);
			if (cartItem != null)
			{
				DbContext.CartItem.Remove(cartItem);
				await DbContext.SaveChangesAsync();
				return true;
			}
			return false;
		}
	}
}
