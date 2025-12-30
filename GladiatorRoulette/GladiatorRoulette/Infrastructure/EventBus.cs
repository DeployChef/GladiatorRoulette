using GladiatorRoulette.Domain.Events;

namespace GladiatorRoulette.Infrastructure;

public class EventBus : IEventBus
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new();
    
    public void Subscribe<T>(Action<T> handler) where T : IDomainEvent
    {
        var type = typeof(T);
        if (!_handlers.ContainsKey(type)) _handlers[type] = new List<Delegate>();
        _handlers[type].Add(handler);
    }
    
    public void Unsubscribe<T>(Action<T> handler) where T : IDomainEvent
    {
        var type = typeof(T);
        if (_handlers.ContainsKey(type)) _handlers[type].Remove(handler);
    }
    
    public void Publish<T>(T @event) where T : IDomainEvent
    {
        var type = typeof(T);
        if (_handlers.ContainsKey(type))
        {
            foreach (var handler in _handlers[type].ToList())
            {
                ((Action<T>)handler).Invoke(@event);
            }
        }
    }
}