namespace GladiatorRoulette.Domain.Events;

public class FightFinished : IDomainEvent
{
    public FightFinished(Gladiator winner)
    {
        Winner = winner;
    }

    public Gladiator Winner { get; private set; }
}