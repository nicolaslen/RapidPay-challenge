namespace RapidPay.Application.DTOs;

public class LoginResponseDto
{
    public LoginResponseDto(bool succeeded, string message)
    {
        Succeeded = succeeded;
        Message = message;
        Token = string.Empty;
    }

    public LoginResponseDto(string token)
    {
        Succeeded = true;
        Message = string.Empty;
        Token = token;
    }

    public bool Succeeded { get; }
    public string Message { get; }
    public string Token { get; }
}