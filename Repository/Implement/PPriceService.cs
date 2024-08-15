using Storee.Entities;
using Store.Repository.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Repository.Implement
{
	public class PPriceService:IPPriceService
	{
		private readonly IPPriceRepository PPriceRepository;

		public PPriceService(IPPriceRepository PPriceRepository)
		{
			this.PPriceRepository = PPriceRepository;
		}

		public Task<ProductPrice> CreateProductPriceAsync(ProductPrice ProductPrice)
		{
			return PPriceRepository.CreateProductPriceAsync(ProductPrice);
		}

		public Task<bool> DeleteProductPriceAsync(int id)
		{
			return PPriceRepository.DeleteProductPriceAsync(id);
		}

		public async Task<ProductPrice> GetPricesForCountryAsync(int countryId)
		{
			return await PPriceRepository.GetPricesForCountryAsync(countryId);
		}


		public Task<ProductPrice> GetProductPriceAsync(int? adObjName, int? productId)
		{
			return PPriceRepository.GetProductPriceAsync(adObjName, productId);
		}

		public Task<ProductPrice> GetProductPriceAsync(int id)
		{
			return PPriceRepository.GetProductPriceAsync(id);
		}

		public Task<List<ProductPrice>> GetProductPricesAsync()
		{
			return PPriceRepository.GetProductPricesAsync();
		}

		public Task<bool> IsProductPriceExistAsync(int productPriceId)
		{
			return PPriceRepository.IsProductPriceExistAsync(productPriceId);
		}

		public Task<bool> IsProductPriceExistAsync(int? countryId, int? productId)
		{
			return PPriceRepository.IsProductPriceExistAsync(countryId, productId);
		}
	}
}
