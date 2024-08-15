using Storee.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Abstract
{
	public interface IPaymentRepository
	{
		Task CreatePaymentAsync(Payment payment);
		Task<Payment> GetPaymentByOrderIdAsync(int orderId);
		Task UpdatePaymentAsync(Payment payment);
	}
}
