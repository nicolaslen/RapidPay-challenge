using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RapidPay.Application.Interfaces.Infrastructure;
using RapidPay.Infrastructure.Authentication;
using RapidPay.Infrastructure.Repositories;
using Serilog;

namespace RapidPay.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IHostBuilder host,
        IConfiguration configuration)
    {
        host.UseSerilog();

        services.AddDbContext<RapidPayDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RapidPayDbConnection")));

        services.AddAuthenticationServices(configuration);

        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        return services;
    }
}