using Microsoft.EntityFrameworkCore;
using Storee.Data;
using Storee.Entities;
using Storee.Repository.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Implement
{
	public class RepositoryHome : IRepositoryHome
	{
		private readonly StoreDbContext DbContext;
		public RepositoryHome(StoreDbContext DbContext)
		{
			this.DbContext = DbContext;
		}
		public async Task<Home> CreateHomeAsync(Home home)
		{
			DbContext.Home.Add(home);
			await DbContext.SaveChangesAsync();
			return home;
		}

		public async Task<bool> DeleteHomeAsync(short homeId)
		{
			var categoryToRemove = await DbContext.Home.FindAsync(homeId);
			DbContext.Home.Remove(categoryToRemove);
			return await DbContext.SaveChangesAsync() > 0;
		}

		public async Task<Home> GetHomeAndProductsAsync(short homeId)
		{
			var category = await DbContext.Home
		.Include(c => c.Product)
			.ThenInclude(p => p.Price) // تأكد من تضمين أسعار المنتجات
		.FirstOrDefaultAsync(c => c.Id == homeId);

			return category;
		}

		public Task<Home> GetHomeAsync(string name)
		{
			return this.DbContext.Home.FirstOrDefaultAsync(f => f.enTitle.ToLower() == name.ToLower());
		}

		public Task<Home> GetHomeAsync(short homeId)
		{
			return this.DbContext.Home.FindAsync(homeId).AsTask();
		}

		public Task<List<Home>> GetHomesAsync()
		{
			return this.DbContext.Home.ToListAsync();
		}

		public async Task<Home> UpdateHomeAsync(Home home)
		{
			DbContext.Home.Update(home);
			await DbContext.SaveChangesAsync();
			return home;
		}
	}
}
