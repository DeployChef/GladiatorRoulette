using GladiatorRoulette.Application;
using GladiatorRoulette.Application.Enums;
using GladiatorRoulette.ConsoleRunner;
using GladiatorRoulette.Domain;
using GladiatorRoulette.Domain.Events;
using GladiatorRoulette.Domain.Rules;
using GladiatorRoulette.Infrastructure;
using GladiatorRoulette.Infrastructure.Random;

// Program.cs
class Program
{
    static async Task Main()
    {
        // Инициализация
        var eventBus = new EventBus();
        var random = new SystemRandomProvider();
        var fightResolver = new FightRules(random);
        
        var gladiators = new List<Gladiator>
        {
            new("1", "Максимус"),
            new("2", "Спартак"),
            new("3", "Коммод"),
            new("4", "Квинт")
        };
        
        var fight = new Fight(gladiators, fightResolver, eventBus);
        var startFightUseCase = new StartFightUseCase(fight);
        var gameStateMachine = new GameStateMachine(eventBus);
        
        using var consoleView = new ConsoleView(eventBus);
        
        // Инициализация экрана
        consoleView.RenderReset();
        
        // Главный цикл ввода
        while (true)
        {
            if (gameStateMachine.CurrentState != GameState.Idle)
            {
                await Task.Delay(500);
                continue;
            }
            var key = Console.ReadKey();
            
            if (key.Key == ConsoleKey.Escape)
            {
                
                Console.Clear();
                Console.WriteLine("Завершение работы...");
                break;
            }
            
            startFightUseCase.Execute();
        }
    }
}