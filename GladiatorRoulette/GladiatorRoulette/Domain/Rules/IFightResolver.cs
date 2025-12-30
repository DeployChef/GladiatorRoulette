namespace GladiatorRoulette.Domain.Rules;

public interface IFightResolver
{
    Gladiator ResolveWinner(List<Gladiator> gladiators);
}