using Microsoft.Extensions.DependencyInjection;
using RapidPay.Application.Interfaces;
using RapidPay.Application.Services;
using RapidPay.Application.Services.Cards;
using RapidPay.Application.Services.Payments;

namespace RapidPay.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IUniversalFeesExchangeService, UniversalFeesExchangeService>();

        services.AddScoped<ICardService, CardService>();
        services.AddScoped<IPaymentService, PaymentService>();

        return services;
    }
}