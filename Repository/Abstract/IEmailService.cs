using System.Threading.Tasks;

namespace Storee.Repository.Abstract
{
	public interface IEmailService
	{
		Task SendPaymentNotificationAsync(int orderId, string message);
	}
}
