
namespace Shared.DDD;

//public interface IAggregate<T> : IAggregate, IEntity<T>
//{
//}

public interface IAggregate<T> : IEntity<T>
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvents();
}
