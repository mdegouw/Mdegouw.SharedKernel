namespace Mdegouw.SharedKernel;

public interface IHasDomainEvents
{
  IReadOnlyCollection<DomainEventBase> DomainEvents { get; }
}
