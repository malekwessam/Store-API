using Storee.Validation;
using System.Collections.Generic;

namespace Storee.ViewModel.Get
{
    public class CategoryViewModel : AbstractValidatableObject
    {
        public short Id { get; set; }
		public string CategoryNameEn { get; set; }
		public string CategoryNameAr { get; set; }
		public bool IsActive { get; set; } = true;
       

        
        public List<CategoryProductViewModel> CategoryProductViewModels { get; set; }
    }
}
