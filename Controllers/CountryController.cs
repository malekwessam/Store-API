using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Repository.Abstract;
using Storee.Entities;
using Storee.ViewModel.Create;
using Storee.ViewModel.Get;
using Storee.ViewModel.Update;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storee.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController : ControllerBase
	{
		private readonly ICountryService countryService;
		private readonly IHostingEnvironment hostingEnvironment;


		public CountryController(ICountryService countryService, IHostingEnvironment hostingEnvironment)
		{
			this.countryService = countryService;
			this.hostingEnvironment = hostingEnvironment;
		}
		// GET: api/<CountryController>
		[HttpGet("", Name = "GetCountrys")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(List<Country1ViewModel>), StatusCodes.Status200OK)]
		public async Task<ActionResult> Get()
		{

			var countrys = await countryService.GetCountrysAsync();

			var models = countrys.Select(country => new Country1ViewModel()
			{

				Id = country.Id,
				Name = country.Name == null ? null : country.Name,

				pathImage = country.PathImage == null ? null : "http://www.AqtanPure.somee.com/" + country.PathImage,
				


			}).ToList();
			return Ok(models);
		}

		// GET api/<CountryController>/5
		[HttpGet("{id}", Name = "GetCountry")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(CountryViewModel), StatusCodes.Status200OK)]
		public async Task<ActionResult> Get(int id)

		{

			var category = await countryService.GetCountryAndProductPriceAsync(id);
			if (category == null)
				return NotFound();
			var nn = "http://www.AqtanPure.somee.com/" + category.PathImage;

			var model = new CountryViewModel()
			{
				Id = category.Id,
				Name = category.Name == null ? null : category.Name,
				pathImage = category.PathImage == null ? null : nn,

				

				CountryProductPriceViewModels = category.ProductPrice.Any() ? category.ProductPrice.Select(s => new CountryProductPriceViewModel()
				{


					Id = s.Id,
					CountryId= s.CountryId,
					ProductId= s.ProductId,
					Price= s.Price,

				}).ToList() : new List<CountryProductPriceViewModel>()

			};
			return Ok(model);
		}

		// POST api/<CategoryController>
		[HttpPost]

		public async Task<ActionResult> Post([FromForm] CreateCountry createCountry, IFormFile image)
		{
			Random random = new Random();
			int rNum = random.Next();
			var images = "CountryImages/" + rNum + image.FileName;
			var pathImage = Path.Combine(hostingEnvironment.WebRootPath, images);
			var streamImage = new FileStream(pathImage, FileMode.Append);
			image.CopyTo(streamImage);
			var entityToAdd = new Country()
			{
				//Id=createCategory.Id,
				Name = createCountry.Name,
				PathImage = images,

				

			};
			var createdProduct = await countryService.CreateCountryAsync(entityToAdd);
			return new CreatedAtRouteResult("Get", new { Id = createCountry.Id });
		}

		// PUT api/<CategoryController>/5
	
		// DELETE api/<CategoryController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Delete(short id)
		{
			var category = await countryService.GetCountryAsync(id);
			if (category == null)
				return NotFound();
			//var nn = "http://www.AqtanPure.somee.com/" + category.PathImage;
			//System.IO.File.Delete(nn);
			var isSuccess = await countryService.DeleteCountryAsync(id);

			return Ok();
		}
	}
}
