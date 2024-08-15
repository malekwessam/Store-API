using Storee.Validation;

namespace Storee.ViewModel.Get
{
	public class Category1ViewModel: AbstractValidatableObject
	{
		public short Id { get; set; }
		
		public string CategoryName { get; set; }
		public string CategoryNameAr { get; set; }
		public bool IsActive { get; set; } = true;
		
	}
}
