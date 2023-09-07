using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Application.DTOs;
using RapidPay.Application.Interfaces;

namespace RapidPay.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly ILoggedInUserService _loggedInUserServiceService;

        public CardController(ICardService cardService, ILoggedInUserService loggedInUserService)
        {
            _cardService = cardService;
            _loggedInUserServiceService = loggedInUserService;
        }

        [Authorize]
        [HttpPost(Name = "CreateCard")]
        public async Task<ActionResult> CreateCard(NewCardDto request)
        {
            return Ok(await _cardService.AddCardAsync(request, _loggedInUserServiceService.UserId));
        }

        [Authorize]
        [HttpGet(Name = "GetCardBalance")]
        public async Task<ActionResult<decimal>> GetCardBalance(int id)
        {
            return Ok(await _cardService.GetCardBalanceAsync(id, _loggedInUserServiceService.UserId));
        }
    }
}