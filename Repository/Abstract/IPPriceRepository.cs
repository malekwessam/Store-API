using Storee.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Repository.Abstract
{
	public interface IPPriceRepository
	{
		Task<ProductPrice> GetProductPricessAsync(int? productId, int countryId);
		Task<IEnumerable<ProductPrice>> GetProductPricesByCountryIdAsync(int countryId);
		Task<List<ProductPrice>> GetProductPricesAsync();
		Task<ProductPrice> CreateProductPriceAsync(ProductPrice productPrice);
		Task<bool> IsProductPriceExistAsync(int productPriceId);
		Task<bool> DeleteProductPriceAsync(int id);
		Task<bool> IsProductPriceExistAsync(int? countryId, int? productId);
		Task<ProductPrice> GetProductPriceAsync(int? adObjName, int? productId);
		Task<ProductPrice> GetProductPriceAsync(int id);
		Task<ProductPrice> GetPricesForCountryAsync(int countryId);
	}
}
