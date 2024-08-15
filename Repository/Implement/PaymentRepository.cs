using Microsoft.EntityFrameworkCore;
using Storee.Data;
using Storee.Entities;
using Storee.Repository.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Implement
{
	public class PaymentRepository : IPaymentRepository
	{
		private readonly StoreDbContext DbContext;
		public PaymentRepository(StoreDbContext DbContext)
		{
			this.DbContext = DbContext;
		}
		public async Task CreatePaymentAsync(Payment payment)
		{
			DbContext.Payment.Add(payment);
			await DbContext.SaveChangesAsync();
		}

		public async Task<Payment> GetPaymentByOrderIdAsync(int orderId)
		{
			return await DbContext.Payment.SingleOrDefaultAsync(p => p.OrderId == orderId);
		}

		public async Task UpdatePaymentAsync(Payment payment)
		{
			DbContext.Payment.Update(payment);
			await DbContext.SaveChangesAsync();
		}
	}
}
