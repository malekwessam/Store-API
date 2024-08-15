using Storee.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Repository.Abstract
{
	public interface IProductRepository
	{
		Product GetProduct(int productId);
		List<Product> GetProducts(int noOfProducts);

		Task<Product> GetProductAsync(int productId);
		Task<List<Product>> GetProductsAsync(int noOfProducts);

		Task<Product> CreateProductAsync(Product product);
		Task<Product> UpdateProductAsync(Product product);
		Task<bool> DeleteProductAsync(int productId);
		Task<Product> GetProductByNameAsync(string name);
		Task<Product> GetProductByNameAsync(string name, int id);
		Task<Product> GetProductAndPriceAsync(int productId);
		//Task<Product> GetProductAndSizeAsync(int productId);
		//Task<Product> GetProductAndColorAsync(int productId);
		Task<Product> GetProductAndOrderItemAsync(int productId);
		Task AddProductPriceAsync(ProductPrice productPrice);
	}
}
