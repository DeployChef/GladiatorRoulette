using Domain.Events;

namespace Application.Battle
{
    public class BattleFacade
    {
        private readonly StartFightUseCase _startBattleUseCase;
        private readonly BattleEventQueue _eventQueue;

        public BattleFacade(
            StartFightUseCase startBattleUseCase,
            BattleEventQueue eventQueue)
        {
            _startBattleUseCase = startBattleUseCase;
            _eventQueue = eventQueue;
        }

        public void StartBattle()
        {
            _startBattleUseCase.Execute();
        }

        public void OnDomainEvent(IDomainEvent domainEvent)
        {
            _eventQueue.Enqueue(domainEvent);
        }

        public BattleEventQueue EventQueue => _eventQueue;
    }

}
