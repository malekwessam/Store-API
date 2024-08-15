using Storee.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Repository.Abstract
{
	public interface IProductService
	{
		Product GetProduct(int productId);
		List<Product> GetProducts(int noOfProducts = 100);

		Task<Product> GetProductAsync(int productId);
		Task<List<Product>> GetProductsAsync(int noOfProducts = 100);
		Task<Product> CreateProductAsync(Product product);
		Task<Product> UpdateProductAsync(Product product);
		Task<bool> DeleteProductAsync(int productId);
		Task<bool> IsProductNameExistAsync(string name);
		Task<bool> IsProductExistAsync(string name, int id);
		Task<Product> GetProductAndPriceAsync(int productId);
		//Task<Product> GetProductAndSizeAsync(int productId);
		//Task<Product> GetProductAndColorAsync(int productId);
		Task<Product> GetProductAndOrderItemAsync(int productId);
		Task AddProductPriceAsync(ProductPrice productPrice);
	}
}
