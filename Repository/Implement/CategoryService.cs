using Storee.Entities;
using Store.Repository.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Repository.Implement
{
	public class CategoryService: ICategoryService
	{
		private readonly ICategoryRepository categoryRepository;

		public CategoryService(ICategoryRepository categoryRepository)
		{
			this.categoryRepository = categoryRepository;
		}
		public Task<Category> CreateCategoryAsync(Category category)
		{
			return categoryRepository.CreateCategoryAsync(category);
		}
		public Task<bool> DeleteCategoryAsync(short categoryId)
		{
			return categoryRepository.DeleteCategoryAsync(categoryId);
		}
		public Task<Category> GetCategoryAsync(short categoryId)
		{
			return categoryRepository.GetCategoryAsync(categoryId);
		}

		public Task<List<Category>> GetCategorysAsync()
		{
			return categoryRepository.GetCategorysAsync();
		}
		public Task<Category> UpdateCategoryAsync(Category category)
		{
			return categoryRepository.UpdateCategoryAsync(category);
		}
		public async Task<bool> IsCategoryExistAsync(string name)
		{
			var category = await categoryRepository.GetCategoryAsync(name);
			return category != null;
		}

		public async Task<Category> GetCategoryAndProductsAsync(short categoryId)
		{
			return await categoryRepository.GetCategoryAndProductsAsync(categoryId);
		}
	}
}
