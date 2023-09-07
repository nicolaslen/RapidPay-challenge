using RapidPay.Application.DTOs;

namespace RapidPay.Application.Interfaces.Infrastructure;

public interface IAuthService
{
    Task<RegistrationResponseDto> Registration(RegistrationDto model);
    Task<LoginResponseDto> Login(LoginDto model);
}
