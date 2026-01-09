using System.Collections.Generic;

namespace Domain.Rules
{
    public interface IFightResolver
    {
        Gladiator ResolveWinner(List<Gladiator> gladiators);
    }
}