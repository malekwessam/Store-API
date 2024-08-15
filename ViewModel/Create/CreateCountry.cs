using Storee.Validation;

namespace Storee.ViewModel.Create
{
	public class CreateCountry : AbstractValidatableObject
	{
		public int Id { get; set; }
		public string Name { get; set; }
		
		public string pathImage { get; set; }
	}
}
