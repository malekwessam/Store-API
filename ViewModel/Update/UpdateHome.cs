using Storee.Validation;

namespace Storee.ViewModel.Update
{
	public class UpdateHome : AbstractValidatableObject
	{
		public short Id { get; set; }
		public string arTitle { get; set; }
		public string enTitle { get; set; }
		public string arLargeTitle { get; set; }
		public string enLargeTitle { get; set; }
	}
}
