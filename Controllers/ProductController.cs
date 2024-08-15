using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Storee.ViewModel.Create;
using Storee.ViewModel.Get;
using Storee.ViewModel.Update;
using Store.Repository.Abstract;
using Storee.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Storee.ViewModel.Get;
using Storee.ViewModel.Get;
using Store.Repository.Implement;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storee.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService productService;
		[Obsolete]
		private readonly IHostingEnvironment hostingEnvironment;
		[Obsolete]
		private readonly IWebHostEnvironment _env;
		public ProductController(IProductService productService, IHostingEnvironment hostingEnvironment, IWebHostEnvironment env)
		{
			this.productService = productService;
			this.hostingEnvironment = hostingEnvironment;
			_env = env;
		}
		// GET: api/<ProductController>
		[HttpGet("", Name = "GetProducts")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(List<ProductViewModel>), StatusCodes.Status200OK)]
		public async Task<ActionResult> Get()
		{

			var products = await productService.GetProductsAsync();
			var models = products.Select(product => new ProductViewModel()
			{
				Id = product.Id,
				NameEnglish = product.ProductName == null ? null : product.ProductName,
				NameArabic= product.ProductNameAr == null ? null :product.ProductNameAr,
				pathImage = product.PathImage == null ? null : "http://www.AqtanPure.somee.com/" + product.PathImage,
				DescriptionsEnglish = product.ProductDescription == null ? null : product.ProductDescription,
				DescriptionsArabic=product.ProductDescriptionAr == null ? null :product.ProductDescriptionAr,
				AvailableSince = (DateTime)product.AvailableSince,
				IsActive = (bool)product.IsActive,
				
				
			}).ToList();
			return Ok(models);
		}

		// GET api/<ProductController>/5
		[HttpGet("{id}", Name = "GetProduct")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(Product1ViewModel), StatusCodes.Status200OK)]
		public async Task<ActionResult> GetProduct(int id)
		{
			var product = await productService.GetProductAndPriceAsync(id);
			if (product == null)
				return NotFound();

			var productViewModel = new Product1ViewModel()
			{
				Id = product.Id,
				NameEnglish = product.ProductName,
				NameArabic = product.ProductNameAr,
				DescriptionsEnglish = product.ProductDescription,
				DescriptionsArabic = product.ProductDescriptionAr,
				pathImage = "http://www.AqtanPure.somee.com/" + product.PathImage,
				AvailableSince = (DateTime)product.AvailableSince,
				IsActive = (bool)product.IsActive,
				ImageUrls = product.ImageUrls,
				Colors = product.Colors,
				Sizes = product.Sizes,
			    IsNew = (bool)product.IsNew,
				IsSale = (bool)product.IsSale,
				salePercentage = product.SalePercentage,
				CategoryId = (short)product.CategoryId,
				//HomeId= (short)product.HomeId,
				ProductAndPriceViewModels = product.Price.Any() ? product.Price.Select(s => new ProductAndPriceViewModel()
				{

					Id = s.Id,
					Price=s.Price,
					CountryId=s.CountryId,
					enCurrencyName = s.enCurrencyName,
					arCurrencyName=s.arCurrencyName


				}).ToList() : new List<ProductAndPriceViewModel>()
			};

			return Ok(productViewModel);
		}


		[HttpPost("", Name = "CreateProduct")]
		public async Task<ActionResult> Post([FromForm] CreateProduct createProduct, IFormFile image)
		{
			Random random = new Random();
			int rNum = random.Next();
			var images = "PImages/" + rNum + image.FileName;
			var pathImage = Path.Combine(hostingEnvironment.WebRootPath, images);
			using (var streamImage = new FileStream(pathImage, FileMode.Append))
			{
				image.CopyTo(streamImage);
			}

			var imagePaths = new List<string>();
			if (createProduct.ImageUrls != null && createProduct.ImageUrls.Count > 0)
			{
				foreach (var imageurl in createProduct.ImageUrls)
				{
					var uploadsFolder = Path.Combine(_env.WebRootPath, "Ppics");
					var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageurl.FileName;
					var filePath = Path.Combine(uploadsFolder, uniqueFileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await imageurl.CopyToAsync(fileStream);
					}
					imagePaths.Add("/Ppics/" + uniqueFileName);
				}
			}

			var entityToAdd = new Product()
			{
				ProductName = createProduct.NameEnglish,
				ProductNameAr = createProduct.NameArabic,
				PathImage = images,
				ProductDescription = createProduct.DescriptionsEnglish,
				ProductDescriptionAr = createProduct.DescriptionsArabic,
				CreatedDate = DateTime.Now,
				AvailableSince = DateTime.Now,
				IsActive = createProduct.IsActive,
				CategoryId = createProduct.CategoryId,
				HomeId= createProduct.HomeId,
				ImageUrls = imagePaths.ToArray(),
				Sizes = createProduct.Sizes,
				Colors = createProduct.Colors,
				IsNew=createProduct.isNew,
				IsSale= createProduct.isSale,
				SalePercentage= createProduct.salePercentage,
			};

			// إضافة المنتج
			var createdProduct = await productService.CreateProductAsync(entityToAdd);

			return new CreatedAtRouteResult("Get", new { Id = createdProduct.Id });
		}
		// PUT api/<ProductController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, [FromForm] UpdateProduct updateProduct, IFormFile image,IFormFile pic)
		{
			string images = null;
			if (image != null)
			{
				Random random = new Random();
				int rNum = random.Next();
				images = "PImages/" + rNum + image.FileName;
				var pathImage = Path.Combine(hostingEnvironment.WebRootPath, images);
				var streamImage = new FileStream(pathImage, FileMode.Append);
				image.CopyTo(streamImage);
			}
			var product = await productService.GetProductAsync(id);
			if (product == null) return NotFound();
			var imagePaths = new List<string>(product.ImageUrls);
			if (updateProduct.NewImages != null && updateProduct.NewImages.Count > 0)
			{
				foreach (var image2 in updateProduct.NewImages)
				{
					var uploadsFolder = Path.Combine(_env.WebRootPath, "Ppics");
					var uniqueFileName = Guid.NewGuid().ToString() + "_" + image2.FileName;
					var filePath = Path.Combine(uploadsFolder, uniqueFileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await image2.CopyToAsync(fileStream);
					}
					imagePaths.Add("/Ppics/" + uniqueFileName);
				}
			}

			if (updateProduct.RemovedImagePaths != null && updateProduct.RemovedImagePaths.Length > 0)
			{
				foreach (var removedImagePath in updateProduct.RemovedImagePaths)
				{
					imagePaths.Remove(removedImagePath);
					var filePath = Path.Combine(_env.WebRootPath, removedImagePath.TrimStart('/'));
					if (System.IO.File.Exists(filePath))
					{
						System.IO.File.Delete(filePath);
					}
				}
			}
			var entityToUpdate = await productService.GetProductAsync(updateProduct.Id);



			entityToUpdate.ProductName = updateProduct.NameEnglish;
			entityToUpdate.ProductName = updateProduct.NameArabic;
			entityToUpdate.PathImage = images;
			entityToUpdate.CategoryId = updateProduct.CategoryId;
			

			entityToUpdate.ProductDescription = updateProduct.DescriptionsEnglish;
			entityToUpdate.ProductDescriptionAr = updateProduct.DescriptionsArabic;
			entityToUpdate.IsActive = updateProduct.IsActive;
			entityToUpdate.ImageUrls = updateProduct.ImageUrls;
			entityToUpdate.Sizes = updateProduct.Sizes;
			entityToUpdate.Colors = updateProduct.Colors;
			var updatedProduct = await productService.UpdateProductAsync(entityToUpdate);
			return Ok();
		}


		// DELETE api/<ProductController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Delete(int id)
		{
			var product = await productService.GetProductAsync(id);
			if (product == null)
				return NotFound();
			// حذف مسارات الصور من الخادم
			foreach (var imagePath in product.ImageUrls)
			{
				var filePath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/'));
				if (System.IO.File.Exists(filePath))
				{
					System.IO.File.Delete(filePath);
				}
			}
			// System.IO.File.Delete(product.PathImage);
			var isSuccess = await productService.DeleteProductAsync(id);
			return Ok();
		}
	}
}
