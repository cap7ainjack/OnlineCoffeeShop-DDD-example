using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCoffeeShop.Domain.Common;
public interface IAuditableEntity
{
    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents { get; }
    void ClearDomainEvents();

    public string CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
