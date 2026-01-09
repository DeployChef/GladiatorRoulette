using GladiatorRoulette.Domain;

namespace GladiatorRoulette.Application;

public class StartFightUseCase
{
    private readonly Fight _fight;
    
    public StartFightUseCase(Fight fight) => _fight = fight;
    
    public void 
        Execute() => _fight.Start();
}