using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Storee.Repository.Abstract;
using Storee.ViewModel.Create;
using Storee.ViewModel.Update;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storee.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly IPaymentService _paymentService;
		private readonly ILogger<PaymentController> _logger;

		public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
		{
			_paymentService = paymentService;
			_logger = logger;
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreatePayment([FromForm] CreatePaymentRequest request)
		{
			if (request == null)
			{
				_logger.LogError("CreatePayment request is null");
				return BadRequest("Invalid payment request");
			}

			try
			{
				if (request.PaymentMethod == "cash")
				{
					var result = await _paymentService.CreateCashPaymentAsync(request.OrderId, request.Amount);
					if (result)
					{
						_logger.LogInformation($"Cash payment recorded successfully for OrderId: {request.OrderId}");
						return Ok(new { Message = "Cash payment recorded successfully" });
					}
					_logger.LogWarning($"Failed to record cash payment for OrderId: {request.OrderId}");
					return BadRequest("Failed to record cash payment");
				}
				else if (request.PaymentMethod == "credit_card")
				{
					var paymentIntent = await _paymentService.CreatePaymentIntentAsync(request.OrderId, request.Amount, request.Currency);
					_logger.LogInformation($"Credit card payment initiated successfully for OrderId: {request.OrderId}");
					return Ok(new { ClientSecret = paymentIntent.ClientSecret });
				}
				else
				{
					_logger.LogWarning("Invalid payment method: " + request.PaymentMethod);
					return BadRequest("Invalid payment method");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error processing payment for OrderId: " + request.OrderId);
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpPost("updateStatus")]
		public async Task<IActionResult> UpdatePaymentStatus([FromForm] UpdatePaymentStatusRequest request)
		{
			if (request == null)
			{
				_logger.LogError("UpdatePaymentStatus request is null");
				return BadRequest("Invalid payment status update request");
			}

			try
			{
				var result = await _paymentService.UpdatePaymentStatusAsync(request.OrderId, request.PaymentStatus);
				if (result)
				{
					_logger.LogInformation($"Payment status updated successfully for OrderId: {request.OrderId}");
					return Ok(new { Message = "Payment status updated successfully" });
				}
				_logger.LogWarning($"Failed to update payment status for OrderId: {request.OrderId}");
				return BadRequest("Failed to update payment status");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error updating payment status for OrderId: " + request.OrderId);
				return StatusCode(500, "Internal server error");
			}
		}
	}
}
