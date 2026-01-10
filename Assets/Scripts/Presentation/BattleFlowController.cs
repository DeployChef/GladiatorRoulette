using Application.Battle;
using Domain;
using Domain.Events;
using System.Collections;
using UnityEngine;

namespace Presentation
{
    public class BattleFlowController : MonoBehaviour
    {
        [SerializeField] private GladiatorsViewController gladiators;
        [SerializeField] private ArenaEffectsController effects;

        public IEnumerator PlayBattle(BattleEventQueue queue)
        {
            while (queue.HasEvents)
            {
                var e = queue.Dequeue();

                if (e is FightStarted)
                    yield return PlayBattleStarted();

                if (e is FightFinished finished)
                    yield return PlayBattleFinished(finished);
            }
        }

        private IEnumerator PlayBattleStarted()
        {
            yield return gladiators.JumpAllToCenter();
            effects.PlayDust();
            yield return gladiators.FidgetInCenter();
            yield return null;
        }

        private IEnumerator PlayBattleFinished(FightFinished e)
        {
            var losers = gladiators.GetLosers(e.Winner.Id);

            foreach (var loser in losers)
            {
                yield return effects.ThrowOut(loser);
            }

            yield return gladiators.PlayVictory(e.Winner.Id);
        }
    }

}
