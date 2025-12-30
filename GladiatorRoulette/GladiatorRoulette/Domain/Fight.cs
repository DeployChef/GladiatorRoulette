using GladiatorRoulette.Domain.Events;
using GladiatorRoulette.Domain.Rules;
using GladiatorRoulette.Infrastructure;

namespace GladiatorRoulette.Domain;

// Domain/Models/Fight.cs
public class Fight
{
    private readonly List<Gladiator> _gladiators;
    private readonly IFightResolver _resolver;
    private readonly IEventBus _eventBus;
    
    public Fight(List<Gladiator> gladiators, IFightResolver resolver, IEventBus eventBus)
    {
        _gladiators = gladiators;
        _resolver = resolver;
        _eventBus = eventBus;
    }
    
    public void Start()
    {
        // Сброс и запуск боя
        _gladiators.ForEach(g => g.Reset());
        _eventBus.Publish(new FightStarted());
        
        // Мгновенное разрешение боя
        var winner = _resolver.ResolveWinner(_gladiators);
        _gladiators.Where(g => g != winner).ToList().ForEach(g => g.Eliminate());
        
        _eventBus.Publish(new FightFinished(winner));
    }
}