
using OnlineCoffeeShop.Domain.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCoffeeShop.Domain.Common;
public abstract class Entity<TId> : IAuditableEntity
    where TId : struct
{
    private readonly List<BaseEvent> _domainEvents = new();
    
    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public TId Id { get; private set; } = default;

    private string createdBy;
    private string modifiedBy;

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    public string CreatedBy
    {
        get => this.createdBy;
        set => this.createdBy = value ?? throw new InvalidEntityException("User ID cannot be null.");
    }

    public DateTime CreatedOn { get; set; }

    public string ModifiedBy
    {
        get => this.modifiedBy;
        set => this.modifiedBy = value ?? throw new InvalidEntityException("User ID cannot be null.");
    }

    public DateTime? ModifiedOn { get; set; }

    public override bool Equals(object? obj)
    {
        if (!(obj is Entity<TId> other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (this.GetType() != other.GetType())
        {
            return false;
        }

        if (this.Id.Equals(default) || other.Id.Equals(default))
        {
            return false;
        }

        return this.Id.Equals(other.Id);
    }

    public static bool operator ==(Entity<TId>? first, Entity<TId>? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Equals(second);
    }

    public static bool operator !=(Entity<TId>? first, Entity<TId>? second) => !(first == second);

    public override int GetHashCode() => (this.GetType().ToString() + this.Id).GetHashCode();
}
