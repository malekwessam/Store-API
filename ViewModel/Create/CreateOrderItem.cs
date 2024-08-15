using Storee.ViewModel.Update;

namespace Storee.ViewModel.Create
{
	public class CreateOrderItem
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
