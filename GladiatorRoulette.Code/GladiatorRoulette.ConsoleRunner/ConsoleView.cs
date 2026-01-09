using GladiatorRoulette.Application;
using GladiatorRoulette.Domain;
using GladiatorRoulette.Domain.Events;
using GladiatorRoulette.Domain.Rules;
using GladiatorRoulette.Infrastructure;
using GladiatorRoulette.Infrastructure.Random;

namespace GladiatorRoulette.ConsoleRunner;

public class ConsoleView : IDisposable
{
    private readonly IEventBus _eventBus;
    private readonly Queue<IDomainEvent> _eventQueue = new();
    private readonly object _lock = new();
    private bool _isRendering = false;
    private CancellationTokenSource _cts = new();
    private Task _task;
    
    public ConsoleView(IEventBus eventBus)
    {
        _eventBus = eventBus;
        SubscribeToEvents();
        StartRenderLoop();
    }
    
    private void SubscribeToEvents()
    {
        _eventBus.Subscribe<FightStarted>(OnFightStarted);
        _eventBus.Subscribe<FightFinished>(OnFightFinished);
        _eventBus.Subscribe<FightReset>(OnFightReset);
    }
    
    private void EnqueueEvent(IDomainEvent @event)
    {
        lock (_lock)
        {
            _eventQueue.Enqueue(@event);
        }
    }
    
    private void OnFightStarted(FightStarted e) => EnqueueEvent(e);
    private void OnFightFinished(FightFinished e) => EnqueueEvent(e);
    private void OnFightReset(FightReset e) => EnqueueEvent(e);
    
    private void StartRenderLoop()
    {
        _task =Task.Run(async () =>
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                if (_eventQueue.Count > 0 && !_isRendering)
                {
                    _isRendering = true;
                    
                    IDomainEvent nextEvent;
                    lock (_lock)
                    {
                        nextEvent = _eventQueue.Dequeue();
                    }
                    
                    await ProcessEventAsync(nextEvent);
                    
                    _isRendering = false;
                }
                
                await Task.Delay(16); // ~60 FPS
            }
        }, _cts.Token);
    }
    
    private async Task ProcessEventAsync(IDomainEvent @event)
    {
        switch (@event)
        {
            case FightStarted started:
                await RenderFightStartAsync(started);
                break;
                
            case FightFinished finished:
                await RenderFightResultAsync(finished);
                break;
                
            case FightReset _:
                RenderReset();
                break;
        }
    }
    
    private async Task RenderFightStartAsync(FightStarted e)
    {
        Console.Clear();
        
        // Заголовок боя
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║        ГЛАДИАТОРСКАЯ РУЛЕТКА           ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.ResetColor();
        
        Console.WriteLine("\n\n");
        
        // Участники
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n\n");
        
        // Анимация пыльного облака
        Console.ForegroundColor = ConsoleColor.Gray;
        await AnimateDustCloudAsync();
        Console.ResetColor();
    }
    
    private async Task AnimateDustCloudAsync()
    {
        var frames = new[]
        {
            @"    ████████████████    ",
            @"   ██████████████████   ",
            @"  ████████████████████  ",
            @" ██████████████████████ ",
            @"████████████████████████",
            @" ██████████████████████ ",
            @"  ████████████████████  ",
            @"   ██████████████████   ",
            @"    ████████████████    "
        };
        
        var positions = new[] { 8, 7, 6, 5, 4, 3, 2, 1, 0 };
        
        Console.CursorVisible = false;
        
        for (int i = 0; i < 3; i++) // 3 цикла анимации
        {
            foreach (var (frame, pos) in frames.Zip(positions, (f, p) => (f, p)))
            {
                Console.SetCursorPosition(0, Console.CursorTop - pos);
                Console.WriteLine($"   {frame}");
                Console.SetCursorPosition(0, Console.CursorTop + pos - 1);
                await Task.Delay(50);
            }
        }
        
        Console.CursorVisible = true;
    }
    
    private async Task RenderFightResultAsync(FightFinished e)
    {
        Console.Clear();
        
        // Анимация определения победителя
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n\n\n");
        Console.WriteLine("        Определяем победителя...");
        
        for (int i = 0; i < 3; i++)
        {
            Console.Write(".");
            await Task.Delay(300);
        }
        
        Console.Clear();
        
        // Победитель
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n\n\n");
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║             РЕЗУЛЬТАТ БОЯ             ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.ResetColor();
        
        Console.WriteLine("\n\n");
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"        🏆 ПОБЕДИТЕЛЬ: {e.Winner.Name} 🏆");
        Console.ResetColor();
        
        Console.WriteLine("\n\n");
        
        // Статистика
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("        (результат показывается 2 секунды)");
        Console.ResetColor();
        
        await Task.Delay(2000);
    }

    public void RenderReset()
    {
        Console.Clear();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║        ГЛАДИАТОРСКАЯ РУЛЕТКА           ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.ResetColor();
        
        Console.WriteLine("\n\n");
        Console.WriteLine("   Готов к новому бою!");
        Console.WriteLine("\n");
        
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("   Нажмите любую клавишу для запуска боя...");
        Console.ResetColor();
        
        Console.WriteLine("\n\n");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("   Управление:");
        Console.WriteLine("   • Любая клавиша - начать бой");
        Console.WriteLine("   • ESC - выход из программы");
        Console.ResetColor();
    }
    
    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }
}
