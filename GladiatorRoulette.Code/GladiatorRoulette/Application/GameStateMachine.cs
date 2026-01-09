using GladiatorRoulette.Application.Enums;
using GladiatorRoulette.Domain;
using GladiatorRoulette.Domain.Events;
using GladiatorRoulette.Infrastructure;

namespace GladiatorRoulette.Application;

public class GameStateMachine
{
    private readonly IEventBus _eventBus;
    
    public GameState CurrentState { get; private set; } = GameState.Idle;
    
    public GameStateMachine(IEventBus eventBus)
    {
        _eventBus = eventBus;
        
        _eventBus.Subscribe<FightStarted>(OnFightStarted);
        _eventBus.Subscribe<FightFinished>(OnFightFinished);
    }
    
    private void OnFightStarted(FightStarted e)
    {
        CurrentState = GameState.Fighting;
    }
    
    private void OnFightFinished(FightFinished e)
    {
        CurrentState = GameState.Result;
        // Через 2 секунды автоматически сбросится в Idle
        Task.Delay(2000).ContinueWith(_ => 
        {
            CurrentState = GameState.Idle;
            _eventBus.Publish(new FightReset());
        });
    }
}