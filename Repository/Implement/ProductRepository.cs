using Microsoft.EntityFrameworkCore;
using Storee.Data;
using Storee.Entities;
using Store.Repository.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Repository.Implement
{
	public class ProductRepository:IProductRepository
	{
		private readonly StoreDbContext DbContext;
		public ProductRepository(StoreDbContext DbContext)
		{
			this.DbContext = DbContext;
		}
		public async Task<Product> CreateProductAsync(Product product)
		{
			// إضافة المنتج إلى قاعدة البيانات
			var createdProduct = await DbContext.Product.AddAsync(product);
			await DbContext.SaveChangesAsync();

			// إضافة الأسعار للمنتج
			if (product.Price != null && product.Price.Any())
			{
				foreach (var price in product.Price)
				{
					price.ProductId = createdProduct.Entity.Id;
					await DbContext.ProductPrice.AddAsync(price);
				}
				await DbContext.SaveChangesAsync();
			}

			return createdProduct.Entity;
		}

		public async Task<bool> DeleteProductAsync(int productId)
		{
			var productToRemove = await DbContext.Product.FindAsync(productId);
			DbContext.Remove(productToRemove);

			return await DbContext.SaveChangesAsync() > 0;
		}

		public Product GetProduct(int productId)
		{
			return this.DbContext.Product.Find(productId);
		}

		public Task<Product> GetProductAsync(int productId)
		{
			return this.DbContext.Product.FindAsync(productId).AsTask();
		}

		public Task<Product> GetProductByNameAsync(string name)
		{
			return this.DbContext.Product.FirstOrDefaultAsync(f => f.ProductName.ToLower() == name.ToLower());
		}

		public List<Product> GetProducts(int noOfProducts = 100)
		{
			var products = this.DbContext.Product.OrderByDescending(o => o.CreatedDate).Take(noOfProducts).ToList();
			return products;
		}

		public Task<List<Product>> GetProductsAsync(int noOfProducts = 100)
		{
			var products = DbContext.Product.OrderByDescending(o => o.CreatedDate).Take(noOfProducts).ToListAsync();
			return products;
		}

		public Task<Product> GetProductByNameAsync(string name, int id)
		{
			return DbContext.Product.AsNoTracking().FirstOrDefaultAsync(f => f.ProductName.ToLower() == name.ToLower() && f.Id != id);
		}

		public async Task<Product> UpdateProductAsync(Product product)
		{
			DbContext.Product.Update(product);
			await DbContext.SaveChangesAsync();
			return product;
		}

		public Task<Product> GetProductAndPriceAsync(int productId)
		{
			return DbContext.Product.AsNoTracking().Include(i => i.Price).FirstOrDefaultAsync(f => f.Id == productId);
		}
		//public Task<Product> GetProductAndSizeAsync(int productId)
		//{
		//	return DbContext.Product.AsNoTracking().Include(i => i.ProductSize).FirstOrDefaultAsync(f => f.Id == productId);
		//}
		//public Task<Product> GetProductAndColorAsync(int productId)
		//{
		//	return DbContext.Product.AsNoTracking().Include(i => i.ProductColor).FirstOrDefaultAsync(f => f.Id == productId);
		//}
		public Task<Product> GetProductAndOrderItemAsync(int productId)
		{
			return DbContext.Product.AsNoTracking().Include(i => i.OrderItem).FirstOrDefaultAsync(f => f.Id == productId);
		}

		public async Task AddProductPriceAsync(ProductPrice productPrice)
		{
			DbContext.ProductPrice.Add(productPrice);
			await DbContext.SaveChangesAsync();
		}
	}
}
