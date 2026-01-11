using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Presentation
{
    public class ArenaEffectsController : MonoBehaviour
    {
        [SerializeField] private ArenaView arenaView;
        [SerializeField] private GameObject dustPrefab; // объект Dust или ParticleSystem
        [SerializeField] private Transform dustPosition;

        public void PlayDust()
        {
            var dust = Instantiate(dustPrefab, dustPosition);
            Destroy(dust, 5f); // уничтожаем после окончания анимации
        }

        public IEnumerator ThrowOut(GladiatorView gladiator)
        {
            var target = arenaView.GetRandomOutPoint();
            gladiator.FaceCenter(target.x < arenaView.CenterPoint.x ? arenaView.CenterPoint + new Vector3(10f,10f) : arenaView.CenterPoint - new Vector3(10f, 10f));
            var tween = gladiator.ThrowOut(target);
            yield return tween;
        }
    }

}
