namespace GladiatorRoulette.Infrastructure.Random;

public interface IRandomProvider
{
    int Range(int min, int max);
}