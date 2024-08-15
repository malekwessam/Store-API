namespace Storee.ViewModel.Create
{
	public class CreatePaymentRequest
	{
		public int OrderId { get; set; }
		public decimal Amount { get; set; }
		public string PaymentMethod { get; set; }
		public string Currency { get; set; } // إضافة حقل العملة
		public string CardNumber { get; set; } // للمدفوعات عبر البطاقة
		public string ExpirationDate { get; set; } // للمدفوعات عبر البطاقة
		public string Cvv { get; set; } // للمدفوعات عبر البطاقة
	}
}
