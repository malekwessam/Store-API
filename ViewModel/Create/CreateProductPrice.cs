using Storee.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using Store.Repository.Abstract;
using Storee.ViewModel.Get;
using Microsoft.Extensions.DependencyInjection;

namespace Storee.ViewModel.Create
{
	public class CreateProductPrice:ProductPriceViewModel
	{
		public override async Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext,
	   CancellationToken cancellation)
		{
			var errors = new List<ValidationResult>();
			var wishlistService = validationContext.GetService<IPPriceService>();
			var userService = validationContext.GetService<ICountryService>();
			var user = await userService.GetCountryAndProductPriceAsync(CountryId);

			if (await wishlistService.IsProductPriceExistAsync(CountryId, ProductId))
			{
				errors.Add(new ValidationResult($"Product id {ProductId} doesn't exist ", new[] { nameof(ProductId) }));
			}
			if (user == null)
			{
				errors.Add(new ValidationResult($"user id {CountryId} doesn't exist", new[] { nameof(CountryId) }));
			}
			return errors;

		}
	}
}
