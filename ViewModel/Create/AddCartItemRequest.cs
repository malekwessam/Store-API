namespace Storee.ViewModel.Create
{
	public class AddCartItemRequest
	{
		public string SessionId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
