using Microsoft.EntityFrameworkCore;
using Store.Repository.Abstract;
using Storee.Data;
using Storee.Entities;
using Storee.Repository.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Repository.Implement
{
	public class CategoryRepository: ICategoryRepository
	{
		private readonly StoreDbContext DbContext;
		public CategoryRepository(StoreDbContext DbContext)
		{
			this.DbContext = DbContext;
		}
		public async Task<Category> CreateCategoryAsync(Category category)
		{
			DbContext.Category.Add(category);
			await DbContext.SaveChangesAsync();
			return category;
		}
		public async Task<bool> DeleteCategoryAsync(short categoryId)
		{
			// this will return entity and that is tracked
			var categoryToRemove = await DbContext.Category.FindAsync(categoryId);
			DbContext.Category.Remove(categoryToRemove);
			return await DbContext.SaveChangesAsync() > 0;
		}

		public async Task<Category> GetCategoryAndProductsAsync(short categoryId)
		{
			var category = await DbContext.Category
		.Include(c => c.Product)
			.ThenInclude(p => p.Price) // تأكد من تضمين أسعار المنتجات
		.FirstOrDefaultAsync(c => c.Id == categoryId);

			return category;
		}

		public Task<Category> GetCategoryAsync(short categoryId)
		{
			return this.DbContext.Category.FindAsync(categoryId).AsTask();
		}

		public Task<Category> GetCategoryAsync(string name)
		{
			return this.DbContext.Category.FirstOrDefaultAsync(f => f.CategoryName.ToLower() == name.ToLower());
		}
		public Task<List<Category>> GetCategorysAsync()
		{
			return this.DbContext.Category.ToListAsync();
		}

		public async Task<Category> UpdateCategoryAsync(Category category)
		{
			DbContext.Category.Update(category);
			await DbContext.SaveChangesAsync();
			return category;
		}
	}
}

