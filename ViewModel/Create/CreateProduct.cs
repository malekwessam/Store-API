using Storee.Repository.Abstract;
using Storee.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Store.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Storee.ViewModel.Get;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storee.ViewModel.Create
{
    public class CreateProduct : AbstractValidatableObject
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string NameEnglish { get; set; }
		[Required]
		[MinLength(3)]
		public string NameArabic { get; set; }
		[Required]

        public string DescriptionsEnglish { get; set; }
		[Required]

		public string DescriptionsArabic { get; set; }
		//public string pathImage { get; set; }

        public bool IsActive { get; set; } = true;
        public bool isNew { get; set; }
        public bool isSale { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal salePercentage { get; set; }
		public List<IFormFile> ImageUrls { get; set; }
		public List<string> Colors { get; set; }
		public List<string> Sizes { get; set; }
		
		public short CategoryId { get; set; }
		public short? HomeId { get; set; }
		//public List<ProductPriceViewModel> Prices { get; set; }






		public override async Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext,
      CancellationToken cancellation)
        {
            var errors = new List<ValidationResult>();
            var categoryService = validationContext.GetService<ICategoryService>();
            var productService = validationContext.GetService<IProductService>();
           // var userService = validationContext.GetService<IUserService>();

            var category = await categoryService.GetCategoryAsync(CategoryId);
           // var user = await userService.GetUserAndProductsAsync(UserId);
            var isProductNameExist = await productService.IsProductNameExistAsync(NameEnglish);

            if (category == null)
            {
                errors.Add(new ValidationResult($"Category id {CategoryId} doesn't exist", new[] { nameof(CategoryId) }));
            }
            //if (user == null)
            //{
            //    errors.Add(new ValidationResult($"user id {UserId} doesn't exist", new[] { nameof(UserId) }));
            //}


            return errors;
        }
    }
}
