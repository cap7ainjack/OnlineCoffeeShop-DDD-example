using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineCoffeeShop.Application.Identity.Interfaces;
using OnlineCoffeeShop.Domain.Aggregates.Order;
using OnlineCoffeeShop.Domain.Aggregates.Product;
using OnlineCoffeeShop.Domain.Common;
using OnlineCoffeeShop.Infrastructure.Identity;
using System.Reflection;

namespace OnlineCoffeeShop.Infrastructure;
internal class OnlineCoffeeShopContext : IdentityDbContext<User>
{
    private readonly ICurrentUser _currentUserService;
    private readonly IMediator _mediator;
    public OnlineCoffeeShopContext(DbContextOptions options, ICurrentUser currentUserService, IMediator mediator)
        : base(options)
    {
        this._currentUserService = currentUserService;
        this._mediator = mediator;
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in this.ChangeTracker.Entries<IAuditableEntity>())
        {
            string userId = this._currentUserService.UserId;

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy ??= userId;
                    entry.Entity.CreatedOn = DateTime.Now;
                    entry.Entity.ModifiedBy = userId;
                    entry.Entity.ModifiedOn = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = userId;
                    entry.Entity.ModifiedOn = DateTime.Now;
                    break;
            }
        }

        var entities = this.ChangeTracker
            .Entries<IAuditableEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await this._mediator.Publish(domainEvent);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
