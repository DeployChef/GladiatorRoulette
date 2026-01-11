using Application.Battle;
using Domain;
using Domain.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation
{
    public class BattleFlowController : MonoBehaviour
    {
        [SerializeField] private GladiatorsViewController gladiators;
        [SerializeField] private ArenaEffectsController effects;
        [SerializeField] private GameObject startButton;

        [SerializeField] private ArenaAudioController arenaAudioController;

        public IEnumerator PlayBattle(BattleEventQueue queue)
        {
            startButton.SetActive(false);
            while (queue.HasEvents)
            {
                var e = queue.Dequeue();

                if (e is FightStarted)
                    yield return PlayBattleStarted();

                if (e is FightFinished finished)
                    yield return PlayBattleFinished(finished);
            }
            startButton.SetActive(true);
        }

        private IEnumerator PlayBattleStarted()
        {
            arenaAudioController.PlayScream();
            yield return gladiators.JumpAllToCenter();
            arenaAudioController.PlayFight();
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

            arenaAudioController.PlayVictory();
            yield return gladiators.PlayVictory(e.Winner.Id);
        }
    }

}
