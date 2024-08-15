namespace Storee.ViewModel.Update
{
	public class UpdateCartItemRequest
	{
		public string SessionId { get; set; }
		public int CartItemId { get; set; }
		public int Quantity { get; set; }
	}
}
