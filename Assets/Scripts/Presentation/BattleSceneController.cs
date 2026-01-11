using Application.Battle;
using Presentation.Data;
using UnityEngine;

namespace Presentation
{
    public class BattleSceneController : MonoBehaviour
    {
        [SerializeField] private BattleFlowController flowController;

        [SerializeField] private GladiatorView gladiatorPrefab;
        [SerializeField] private Transform gladiatorsRoot;
        [SerializeField] private ArenaView arenaView;
        [SerializeField] private GladiatorsViewController gladiatorsController;
        [SerializeField] private ArenaAudioController arenaAudioController;

        private BattleFacade _battleFacade;

        public void Init(BattleFacade battleFacade, GladiatorDatabase database)
        {
            _battleFacade = battleFacade;

            var factory = new GladiatorsFactory(gladiatorPrefab, gladiatorsRoot);

            var views = factory.Create(database.AllGladiators);

            gladiatorsController.Init(views);
            gladiatorsController.PlaceInCircle(arenaView);

            arenaAudioController.PlayArenaMusic();
        }

        public void OnFightButtonClicked()
        {
            _battleFacade.StartBattle();
            StartCoroutine(
                flowController.PlayBattle(_battleFacade.EventQueue)
            );
        }
    }
}
