using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Application.DTOs;
using RapidPay.Application.Interfaces;

namespace RapidPay.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILoggedInUserService _loggedInUserServiceService;

        public PaymentController(IPaymentService paymentService, ILoggedInUserService loggedInUserService)
        {
            _paymentService = paymentService;
            _loggedInUserServiceService = loggedInUserService;
        }

        [Authorize]
        [HttpPost(Name = "Pay")]
        public async Task<ActionResult> Pay(NewPaymentDto request)
        {
            return Ok(await _paymentService.AddPaymentAsync(request, _loggedInUserServiceService.UserId));
        }
    }
}