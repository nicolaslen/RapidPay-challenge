using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RapidPay.Domain.Entities;
using System.Reflection;

namespace RapidPay.Infrastructure;

public class RapidPayDbContext : IdentityDbContext<User>
{
    public RapidPayDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Card> Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}