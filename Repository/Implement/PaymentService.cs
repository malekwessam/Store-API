using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Store.Repository.Abstract;
using Storee.Entities;
using Storee.Repository.Abstract;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storee.Repository.Implement
{
	public class PaymentService : IPaymentService
	{
		private readonly IPaymentRepository _paymentRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IEmailService _emailService;
		private readonly StripeSettings _stripeSettings;
		private readonly ILogger<PaymentService> _logger;

		public PaymentService(IOptions<StripeSettings> stripeSettings, IPaymentRepository paymentRepository, IOrderRepository orderRepository, IEmailService emailService, ILogger<PaymentService> logger)
		{
			_stripeSettings = stripeSettings?.Value ?? throw new ArgumentNullException(nameof(stripeSettings));
			StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
			_paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
			_orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
			_emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<PaymentIntent> CreatePaymentIntentAsync(int orderId, decimal amount,string currency)
		{
			var order = await _orderRepository.GetOrderByIdAsync(orderId);
			if (order == null)
			{
				_logger.LogError($"Invalid order ID: {orderId}");
				throw new ArgumentException("Invalid order ID");
			}

			try
			{
				var options = new PaymentIntentCreateOptions
				{
					Amount = (long)(amount * 100), // تحويل المبلغ إلى سنتات
					Currency = currency, // العملة التي يتم تحديدها
					PaymentMethodTypes = new List<string> { "card" }
				};

				var service = new PaymentIntentService();
				var paymentIntent = await service.CreateAsync(options);

				// قم بإنشاء إدخال للدفع في قاعدة البيانات
				var payment = new Payment
				{
					OrderId = orderId,
					Amount = amount,
					PaymentIntentId = paymentIntent.Id,
					PaymentStatus = "Pending",
					Currency = currency, // تخزين العملة في قاعدة البيانات
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				};

				await _paymentRepository.CreatePaymentAsync(payment);
				return paymentIntent;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error creating payment intent for OrderId={orderId}: {ex.Message}");
				throw;
			}
			
		}

		public async Task<bool> CreateCashPaymentAsync(int orderId, decimal amount)
		{
			var order = await _orderRepository.GetOrderByIdAsync(orderId);
			if (order == null)
			{
				_logger.LogWarning($"Order with ID {orderId} not found");
				return false; // الطلب غير موجود
			}

			if (amount != order.TotalAmount)
			{
				_logger.LogWarning($"Payment amount {amount} does not match order total {order.TotalAmount}");
				return false; // المبلغ المدفوع لا يتطابق مع إجمالي الطلب
			}

			var payment = new Payment
			{
				OrderId = orderId,
				Amount = amount,
				PaymentStatus = "Completed",
				Currency = "usd", // تأكد من تخزين العملة المستخدمة
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			await _paymentRepository.CreatePaymentAsync(payment);
			return true;
		}

		public async Task<bool> UpdatePaymentStatusAsync(int orderId, string paymentStatus)
		{
			var payment = await _paymentRepository.GetPaymentByOrderIdAsync(orderId);
			if (payment == null)
			{
				_logger.LogWarning($"Payment for OrderId {orderId} not found");
				return false; // الدفع غير موجود
			}

			payment.PaymentStatus = paymentStatus;
			payment.UpdatedAt = DateTime.UtcNow;

			await _paymentRepository.UpdatePaymentAsync(payment);
			return true;
		}

		public async Task<bool> CreateVisaPaymentAsync(int orderId, decimal amount, string cardNumber, string expirationDate, string cvv, string currency)
		{
			// إنشاء Payment Method باستخدام تفاصيل البطاقة
			var cardOptions = new PaymentMethodCreateOptions
			{
				Type = "card",
				Card = new PaymentMethodCardOptions
				{
					Number = cardNumber,
					ExpMonth = int.Parse(expirationDate.Split('/')[0]),
					ExpYear = int.Parse(expirationDate.Split('/')[1]),
					Cvc = cvv,
				},
			};

			var paymentMethodService = new PaymentMethodService();
			var paymentMethod = await paymentMethodService.CreateAsync(cardOptions);

			// إعداد Payment Intent
			var paymentIntentOptions = new PaymentIntentCreateOptions
			{
				Amount = (long)(amount * 100), // Stripe يتطلب المبلغ بالـ cents
				Currency = currency, // العملة التي يتم تحديدها
				PaymentMethod = paymentMethod.Id,
				Confirm = true,
				Metadata = new Dictionary<string, string>
		{
			{ "order_id", orderId.ToString() }
		}
			};

			var paymentIntentService = new PaymentIntentService();
			var paymentIntent = await paymentIntentService.CreateAsync(paymentIntentOptions);

			return paymentIntent.Status == "succeeded";
		}
		}
}