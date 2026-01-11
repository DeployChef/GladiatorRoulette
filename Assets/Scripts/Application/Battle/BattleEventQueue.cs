using System.Collections.Generic;
using Domain.Events;

namespace Application.Battle
{
    public class BattleEventQueue
    {
        private readonly Queue<IDomainEvent> _events = new();

        public void Enqueue(IDomainEvent domainEvent)
        {
            _events.Enqueue(domainEvent);
        }

        public IDomainEvent Dequeue()
        {
            return _events.Dequeue();
        }

        public bool HasEvents => _events.Count > 0;

        public void Clear()
        {
            _events.Clear();
        }
    }

}
