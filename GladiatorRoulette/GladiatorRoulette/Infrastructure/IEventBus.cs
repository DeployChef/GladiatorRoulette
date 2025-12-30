using GladiatorRoulette.Domain.Events;

namespace GladiatorRoulette.Infrastructure;

public interface IEventBus
{
    void Subscribe<T>(Action<T> handler) where T : IDomainEvent;
    void Unsubscribe<T>(Action<T> handler) where T : IDomainEvent;
    void Publish<T>(T @event) where T : IDomainEvent;
}
