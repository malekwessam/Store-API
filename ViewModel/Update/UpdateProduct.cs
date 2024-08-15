using Storee.Repository.Abstract;
using Storee.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Store.Repository.Abstract;
using Microsoft.AspNetCore.Http;

namespace Storee.ViewModel.Update
{
    public class UpdateProduct : AbstractValidatableObject
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string NameEnglish { get; set; }
		[MinLength(3)]
		public string NameArabic { get; set; }
		public string DescriptionsEnglish { get; set; }
		public string DescriptionsArabic { get; set; }
		public bool IsActive { get; set; }
		public string[] ImageUrls { get; set; }
		public List<string> Colors { get; set; }
		public List<string> Sizes { get; set; }
		public List<IFormFile> NewImages { get; set; }
		public string[] RemovedImagePaths { get; set; }
		public short CategoryId { get; set; }
       


















        public override async Task<IEnumerable<ValidationResult>> ValidateAsync(
         ValidationContext validationContext,
         CancellationToken cancellation)
        {
            var errors = new List<ValidationResult>();

            var productService = validationContext.GetService<IProductService>();


            var productEntity = await productService.GetProductAsync(Id);

            if (productEntity == null)
            {
                errors.Add(new ValidationResult($"No such product id {Id} exist", new[] { nameof(Id) }));
            }




            return errors;
        }
    }
}
