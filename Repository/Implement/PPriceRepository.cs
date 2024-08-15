using Microsoft.EntityFrameworkCore;
using Storee.Data;
using Storee.Entities;
using Store.Repository.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Store.Repository.Implement
{
	public class PPriceRepository:IPPriceRepository
	{
		private readonly StoreDbContext DbContext;
		public PPriceRepository(StoreDbContext DbContext)
		{
			this.DbContext = DbContext;
		}
		

		public async Task<ProductPrice> GetProductPricessAsync(int? productId, int countryId)
		{
			return await DbContext.ProductPrice
				.FirstOrDefaultAsync(pp => pp.ProductId == productId && pp.CountryId == countryId);
		}

		public async Task<IEnumerable<ProductPrice>> GetProductPricesByCountryIdAsync(int countryId)
		{
			return await DbContext.ProductPrice
				.Where(pp => pp.CountryId == countryId)
				.ToListAsync();
		}
		public async Task<ProductPrice> CreateProductPriceAsync(ProductPrice ProductPrice)
		{
			DbContext.ProductPrice.Add(ProductPrice);
			await DbContext.SaveChangesAsync();
			return ProductPrice;
		}

		public async Task<bool> DeleteProductPriceAsync(int id)
		{
			var entityToDelete = await DbContext.ProductPrice.FindAsync(id);
			DbContext.ProductPrice.Remove(entityToDelete);
			return await DbContext.SaveChangesAsync() > 0;
		}

		public Task<ProductPrice> GetPricesForCountryAsync(int countryId)
		{

			return DbContext.ProductPrice.AsNoTracking().Include(i => i.Country).FirstOrDefaultAsync(f => f.CountryId == countryId);

		}

		public Task<ProductPrice> GetProductPriceAsync(int? adObjName, int? productId)
		{
			return DbContext.ProductPrice.FirstOrDefaultAsync(f => f.ProductId == productId
		   && f.CountryId == adObjName);
		}

		public Task<ProductPrice> GetProductPriceAsync(int id)
		{
			return this.DbContext.ProductPrice.FindAsync(id).AsTask();
		}

		public Task<List<ProductPrice>> GetProductPricesAsync()
		{
			return DbContext.ProductPrice.ToListAsync();
		}

		public async Task<bool> IsProductPriceExistAsync(int ProductPriceId)
		{
			var entity = await DbContext.ProductPrice.FindAsync(ProductPriceId);
			return entity != null;
		}

		public async Task<bool> IsProductPriceExistAsync(int? countryId, int? productId)
		{
			var ProductPrice = await DbContext.ProductPrice.FirstOrDefaultAsync(f => f.ProductId == productId && f.CountryId == countryId);
			return ProductPrice != null;
		}
	}
}
