using System.Collections.Generic;
using System.Linq;
using Infrastructure.Random;

namespace Domain.Rules
{
    public class FightRules : IFightResolver
    {
        private readonly IRandomProvider _random;
        
        public FightRules(IRandomProvider random) => _random = random;
        
        public Gladiator ResolveWinner(List<Gladiator> gladiators)
        {
            var alive = gladiators.Where(g => g.IsAlive).ToList();
            return alive[_random.Range(0, alive.Count)];
        }
    }
}