namespace Storee.ViewModel.Update
{
	public class UpdatePaymentStatusRequest
	{
		public int OrderId { get; set; }
		public string PaymentStatus { get; set; }
	}
}
