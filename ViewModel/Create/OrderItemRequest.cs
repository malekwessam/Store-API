namespace Storee.ViewModel.Create
{
	public class OrderItemRequest
	{
		public int ProductId { get; set; } // معرّف المنتج
		public int Quantity { get; set; } // الكمية المطلوبة
		public decimal UnitPrice { get; set; }
	}
}

