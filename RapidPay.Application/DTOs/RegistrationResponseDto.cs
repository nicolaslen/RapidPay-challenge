namespace RapidPay.Application.DTOs;

public class RegistrationResponseDto
{
    public RegistrationResponseDto(string message)
    {
        Succeeded = false;
        Message = message;
    }

    public RegistrationResponseDto()
    {
        Succeeded = true;
        Message = string.Empty;
    }

    public bool Succeeded { get; }
    public string Message { get; }
}