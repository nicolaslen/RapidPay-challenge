using RapidPay.Application.Interfaces;
using System.Security.Claims;

namespace RapidPay.Api;
public class LoggedInUserService : ILoggedInUserService
{
    private readonly IHttpContextAccessor _contextAccessor;
    public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
    {
        _contextAccessor = httpContextAccessor;
    }

    public string UserId => _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}