using System;
using Application.Enums;
using Domain.Events;
using Infrastructure;

namespace Application
{
    public class GameStateMachine
{
    private readonly IEventBus _eventBus;
    private readonly Action<float, Action> _delayAction;
    
    public GameState CurrentState { get; private set; } = GameState.Idle;
    
    public GameStateMachine(IEventBus eventBus, Action<float, Action> delayAction)
    {
        _eventBus = eventBus;
        _delayAction = delayAction;
        
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
        _delayAction?.Invoke(2f, () =>
        {
            CurrentState = GameState.Idle;
            _eventBus.Publish(new FightReset());
        });
    }
    }
}