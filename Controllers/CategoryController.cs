using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Repository.Abstract;
using Storee.Entities;
using Storee.ViewModel.Create;
using Storee.ViewModel.Get;
using Storee.ViewModel.Update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storee.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService categoryService;
		private readonly ILogger<CategoryController> _logger;
		private readonly IHostingEnvironment hostingEnvironment;

		public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger, IHostingEnvironment hostingEnvironment)
		{
			this.categoryService = categoryService;
			this.hostingEnvironment = hostingEnvironment;
			_logger = logger;
		}
		// GET: api/<CategoryController>
		[HttpGet("", Name = "GetCategorys")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(List<Category1ViewModel>), StatusCodes.Status200OK)]
		public async Task<ActionResult> Get()
		{

			var categorys = await categoryService.GetCategorysAsync();

			var models = categorys.Select(category => new Category1ViewModel()
			{

				Id = category.Id,
				CategoryName = category.CategoryName == null ? null : category.CategoryName,
				CategoryNameAr = category.CategoryNameAr == null ? null : category.CategoryNameAr,
				//	pathImage = category.PathImage == null ? null : "http://www.AqtanPure.somee.com/" + category.PathImage,
				IsActive = (bool)category.IsAcTive


			}).ToList();
			return Ok(models);
		}

		// GET api/<CategoryController>/5
		[HttpGet("{id}", Name = "GetCategory")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(CategoryViewModel), StatusCodes.Status200OK)]
		public async Task<ActionResult> Get(short id)
		{
			var category = await categoryService.GetCategoryAndProductsAsync(id);
			if (category == null)
				return NotFound();

			var nn = "http://www.Store.somee.com/" + category.PathImage;

			var model = new CategoryViewModel()
			{
				Id = category.Id,
				CategoryNameEn = category.CategoryName,
				CategoryNameAr = category.CategoryNameAr,
				IsActive = category.IsAcTive ?? false,
				CategoryProductViewModels = category.Product.Any()
					? category.Product.Select(s => new CategoryProductViewModel()
					{
						Id = s.Id,
						enProductName = s.ProductName,
						arProductName = s.ProductNameAr,
						enDescription = s.ProductDescription,
						arDescription = s.ProductDescriptionAr,
						pathImage = "http://www.AqtanPure.somee.com/" + s.PathImage,
						AvailableSince = s.AvailableSince ?? DateTime.MinValue,
						IsActive = s.IsActive ?? false,
						IsNew = s.IsNew ?? false,
						IsSale = s.IsSale ?? false,
						SalePercentage = s.SalePercentage ?? 0M,
						Price = s.Price.Select(p => new ProductPriceViewModel
						{
							Id = p.Id,
							ProductId= p.ProductId,
							enCurrencyName=p.enCurrencyName,
							arCurrencyName=p.arCurrencyName,
							CountryId = p.CountryId,
							Price = p.Price,
							
						}).ToList()
					}).OrderByDescending(o => o.AvailableSince).ToList()
					: new List<CategoryProductViewModel>()
			};

			// للتحقق من البيانات
			foreach (var product in category.Product)
			{
				var prices = product.Price; // افحص إذا كانت فارغة
				if (!prices.Any())
				{
					_logger.LogWarning($"Product {product.Id} has no prices.");
				}
			}

			return Ok(model);
		}

		// POST api/<CategoryController>
		[HttpPost]

		public async Task<ActionResult> Post([FromForm] CreateCategory createCategory, IFormFile image)
		{
			Random random = new Random();
			int rNum = random.Next();
			var images = "images/" + rNum + image.FileName;
			var pathImage = Path.Combine(hostingEnvironment.WebRootPath, images);
			var streamImage = new FileStream(pathImage, FileMode.Append);
			image.CopyTo(streamImage);
			var entityToAdd = new Category()
			{
				//Id=createCategory.Id,
				CategoryName = createCategory.NameEn,
				CategoryNameAr=createCategory.NameAr,
				PathImage = images,

				IsAcTive = createCategory.IsActive

			};
			var createdProduct = await categoryService.CreateCategoryAsync(entityToAdd);
			return new CreatedAtRouteResult("Get", new { Id = createCategory.Id });
		}

		// PUT api/<CategoryController>/5
		[HttpPut("{id}", Name = "UpdateCategory")]

		public async Task<ActionResult> Put(int id, [FromForm] UpdateCategory updateCategory, IFormFile image)
		{



			string images = null;
			if (image != null)
			{
				Random random = new Random();
				int rNum = random.Next();
				images = "images/" + rNum + image.FileName;
				var pathImage = Path.Combine(hostingEnvironment.WebRootPath, images);
				var streamImage = new FileStream(pathImage, FileMode.Append);
				image.CopyTo(streamImage);
			}

			var entityToUpdate = await categoryService.GetCategoryAsync(updateCategory.Id);


			entityToUpdate.Id = updateCategory.Id;
			entityToUpdate.CategoryName = updateCategory.Name;

			entityToUpdate.IsAcTive = updateCategory.IsActive;

			entityToUpdate.PathImage = images;


			var updatedCategory = await categoryService.UpdateCategoryAsync(entityToUpdate);
			return Ok();

		}

		// DELETE api/<CategoryController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Delete(short id)
		{
			var category = await categoryService.GetCategoryAsync(id);
			if (category == null)
				return NotFound();
			//var nn = "http://www.moqayda.somee.com/" + category.PathImage;
			//System.IO.File.Delete(nn);
			var isSuccess = await categoryService.DeleteCategoryAsync(id);

			return Ok();
		}
	}
}

