using Storee.Repository.Abstract;
using System.Threading.Tasks;
using System;
using System.Net.Mail;
using System.Net;

namespace Storee.Repository.Implement
{
	public class EmailService : IEmailService
	{
		private readonly string _smtpServer = "smtp.example.com"; // استبدل بعنوان خادم SMTP الخاص بك
		private readonly int _smtpPort = 587; // المنفذ الافتراضي لخادم SMTP
		private readonly string _smtpUser = "your-email@example.com"; // البريد الإلكتروني المستخدم للمصادقة
		private readonly string _smtpPass = "your-email-password"; // كلمة مرور البريد الإلكتروني

		public async Task SendPaymentNotificationAsync(int orderId, string message)
		{
			var mailMessage = new MailMessage
			{
				From = new MailAddress("no-reply@example.com", "Your Company Name"),
				Subject = $"Payment Notification for Order #{orderId}",
				Body = message,
				IsBodyHtml = true
			};

			// هنا يمكنك إضافة عنوان البريد الإلكتروني للعميل، على سبيل المثال:
			mailMessage.To.Add("customer@example.com");

			using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
			{
				smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
				smtpClient.EnableSsl = true; // تأكد من تمكين SSL إذا كان مطلوبًا

				await smtpClient.SendMailAsync(mailMessage);
			}
		}
	}
}
