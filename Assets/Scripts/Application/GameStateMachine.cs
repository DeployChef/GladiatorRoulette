using System;
using Application.Enums;
using Domain.Events;
using Infrastructure;

namespace Application
{
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
            CurrentState = GameState.Idle;
        }
    }
}