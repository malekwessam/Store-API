using Microsoft.EntityFrameworkCore;
using Storee.Data;
using Storee.Entities;
using Store.Repository.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Repository.Implement
{
	public class CountryRepository:ICountryRepository
	{
		private readonly StoreDbContext DbContext;
		public CountryRepository(StoreDbContext DbContext)
		{
			this.DbContext = DbContext;
		}
		public async Task<Country> CreateCountryAsync(Country Country)
		{
			DbContext.Country.Add(Country);
			await DbContext.SaveChangesAsync();
			return Country;
		}
		public async Task<bool> DeleteCountryAsync(int CountryId)
		{
			// this will return entity and that is tracked
			var CountryToRemove = await DbContext.Country.FindAsync(CountryId);
			DbContext.Country.Remove(CountryToRemove);
			return await DbContext.SaveChangesAsync() > 0;
		}

		public Task<Country> GetCountryAndProductPriceAsync(int? CountryId)
		{
			return DbContext.Country.AsNoTracking().Include(i => i.ProductPrice).FirstOrDefaultAsync(f => f.Id == CountryId);
		}

		public Task<Country> GetCountryAsync(int CountryId)
		{
			return this.DbContext.Country.FindAsync(CountryId).AsTask();
		}

		public Task<Country> GetCountryAsync(string name)
		{
			return this.DbContext.Country.FirstOrDefaultAsync(f => f.Name.ToLower() == name.ToLower());
		}
		public Task<List<Country>> GetCountrysAsync()
		{
			return this.DbContext.Country.ToListAsync();
		}

		public async Task<Country> UpdateCountryAsync(Country Country)
		{
			DbContext.Country.Update(Country);
			await DbContext.SaveChangesAsync();
			return Country;
		}
	}
}
