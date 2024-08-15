using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Repository.Abstract;
using Store.Repository.Implement;
using Storee.Entities;
using Storee.ViewModel.Create;
using Storee.ViewModel.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storee.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductPriceController : ControllerBase
	{
		private readonly IPPriceService ppriceService;
		
		public ProductPriceController(IPPriceService ppriceService)
		{
			
			this.ppriceService = ppriceService;
		}
		[HttpGet("all", Name = "GetPrices")]
		[ProducesResponseType(typeof(List<ProductPriceViewModel>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetProductPricesAsync()
		{

			var wishlists = await ppriceService.GetProductPricesAsync();

			var wishlistsViewModel = wishlists.Select(s => new ProductPriceViewModel()
			{
				CountryId = Convert.ToInt32(s.CountryId),
				ProductId = Convert.ToInt32(s.ProductId),
				arCurrencyName=s.arCurrencyName,
				enCurrencyName=s.enCurrencyName,
				Id = s.Id,
				Price=s.Price
			}).ToList();

			return Ok(wishlistsViewModel);
		}

		[HttpGet("{id}", Name = "GetProductPrice")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ProductPriceViewModel), StatusCodes.Status200OK)]
		public async Task<ActionResult> Get(int id)

		{

			var category = await ppriceService.GetProductPriceAsync(id);
			if (category == null)
				return NotFound();
			

			var model = new ProductPriceViewModel()
			{
				Id = category.Id,
				ProductId = category.ProductId == null ? null : category.ProductId,
				CountryId = category.CountryId == null ? null : category.CountryId,
				arCurrencyName= category.arCurrencyName,
				enCurrencyName=category.enCurrencyName,
				Price = category.Price,

				
			};
			return Ok(model);
		}
		[HttpPost("", Name = "CreateProductPrices")]
		public async Task<IActionResult> PostProductPriceAsync([FromForm] CreateProductPrice createProductPrice)
		{

			var wishListInDB = await ppriceService.GetProductPriceAsync(createProductPrice.CountryId,
			  createProductPrice.ProductId);

			if (wishListInDB == null)
			{
				var entity = new ProductPrice()
				{
					CountryId = createProductPrice.CountryId,
					ProductId = createProductPrice.ProductId,
					arCurrencyName=createProductPrice.arCurrencyName,
					enCurrencyName=createProductPrice.enCurrencyName,
					Price = createProductPrice.Price,
					CreatedDate= DateTime.Now
				};

				var isSuccess = await ppriceService.CreateProductPriceAsync(entity);
				return new CreatedAtRouteResult("GetProductPrice",
				  new { id = entity.Id });
			}
			return new CreatedAtRouteResult("GetProductPrice",
				   new { id = wishListInDB.Id });
		}
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Delete(int id)
		{
			var product = await ppriceService.GetProductPriceAsync(id);
			if (product == null)
				return NotFound();

			// System.IO.File.Delete(product.PathImage);
			var isSuccess = await ppriceService.DeleteProductPriceAsync(id);
			return Ok();
		}
	}
}
