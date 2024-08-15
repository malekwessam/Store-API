using Storee.Entities;
using Stripe;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Abstract
{
	public interface IPaymentService
	{
		    public Task<PaymentIntent> CreatePaymentIntentAsync(int orderId, decimal amount,string currency);
			public Task<bool> CreateCashPaymentAsync(int orderId, decimal amount);
			public Task<bool> UpdatePaymentStatusAsync(int orderId, string paymentStatus);
		    Task<bool> CreateVisaPaymentAsync(int orderId, decimal amount, string cardNumber, string expirationDate, string cvv,string currency);
	}
}
