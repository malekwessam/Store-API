using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Repository.Abstract;
using Storee.Entities;
using Storee.ViewModel.Create;
using Storee.ViewModel.Get;
using Storee.ViewModel.Update;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using Storee.Repository.Abstract;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storee.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HomeController : ControllerBase
	{

		private readonly IServiceHome homeService;
		private readonly ILogger<HomeController> _logger;
		

		public HomeController(IServiceHome homeService, ILogger<HomeController> logger)
		{
			this.homeService = homeService;
			
			_logger = logger;
		}
		// GET: api/<CategoryController>
		[HttpGet("", Name = "GetAllHome")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(List<Home1ViewModel>), StatusCodes.Status200OK)]
		public async Task<ActionResult> Get()
		{

			var categorys = await homeService.GetHomesAsync();

			var models = categorys.Select(category => new Home1ViewModel()
			{

				Id = category.Id,
				enTitle = category.enTitle == null ? null : category.enTitle,
				arTitle = category.arTitle == null ? null : category.arTitle,
				enLargeTitle = category.enLargeTitle == null ? null : category.enLargeTitle,
				arLargeTitle = category.arLargeTitle == null ? null : category.arLargeTitle,


			}).ToList();
			return Ok(models);
		}

		// GET api/<CategoryController>/5
		[HttpGet("{id}", Name = "GetHome")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(HomeViewModel), StatusCodes.Status200OK)]
		public async Task<ActionResult> Get(short id)
		{
			var category = await homeService.GetHomeAndProductsAsync(id);
			if (category == null)
				return NotFound();

			

			var model = new HomeViewModel()
			{
				Id = category.Id,
				enTitle = category.enTitle ,
				arTitle = category.arTitle ,
				enLargeTitle = category.enLargeTitle,
				arLargeTitle = category.arLargeTitle ,
				HomeProductViewModels = category.Product.Any()
					? category.Product.Select(s => new HomeProductViewModel()
					{
						Id = s.Id,
						enProductName = s.ProductName,
						arProductName = s.ProductNameAr,
						enDescription = s.ProductDescription,
						arDescription = s.ProductDescriptionAr,
						pathImage = "http://www.moqayda.somee.com/" + s.PathImage,
						AvailableSince = s.AvailableSince ?? DateTime.MinValue,
						IsActive = s.IsActive ?? false,
						IsNew = s.IsNew ?? false,
						IsSale = s.IsSale ?? false,
						SalePercentage = s.SalePercentage ?? 0M,
						Price = s.Price.Select(p => new ProductPriceViewModel
						{
							Id = p.Id,
							ProductId= p.ProductId,
							enCurrencyName = p.enCurrencyName,
							arCurrencyName = p.arCurrencyName,
							CountryId = p.CountryId,
							Price = p.Price,

						}).ToList()
					}).OrderByDescending(o => o.AvailableSince).ToList()
					: new List<HomeProductViewModel>()
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

		public async Task<ActionResult> Post([FromForm] CreateHome createHome)
		{
			
			var entityToAdd = new Home()
			{
				//Id=createCategory.Id,
				enTitle = createHome.enTitle,
				arTitle = createHome.arTitle,
				enLargeTitle = createHome.enLargeTitle,
				arLargeTitle = createHome.arLargeTitle,

			};
			var createdProduct = await homeService.CreateHomeAsync(entityToAdd);
			return new CreatedAtRouteResult("Get", new { Id = createHome.Id });
		}

		// PUT api/<CategoryController>/5
		[HttpPut("{id}", Name = "UpdateHome")]

		public async Task<ActionResult> Put(int id, [FromForm] UpdateHome updateCategory)
		{



			
			var entityToUpdate = await homeService.GetHomeAsync(updateCategory.Id);


			entityToUpdate.Id = updateCategory.Id;
			entityToUpdate.enTitle = updateCategory.enTitle;

			entityToUpdate.arTitle = updateCategory.arTitle;
			entityToUpdate.enLargeTitle = updateCategory.enLargeTitle;
			entityToUpdate.arLargeTitle = updateCategory.arLargeTitle;


			var updatedCategory = await homeService.UpdateHomeAsync(entityToUpdate);
			return Ok();

		}

		// DELETE api/<CategoryController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Delete(short id)
		{
			var category = await homeService.GetHomeAsync(id);
			if (category == null)
				return NotFound();
			//var nn = "http://www.moqayda.somee.com/" + category.PathImage;
			//System.IO.File.Delete(nn);
			var isSuccess = await homeService.DeleteHomeAsync(id);

			return Ok();
		}
	}
}
