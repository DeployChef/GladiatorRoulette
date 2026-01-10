using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Presentation
{
    public class GladiatorsViewController : MonoBehaviour
    {
        private List<GladiatorView> gladiators = new();
        [SerializeField] private ArenaView arenaView;

        // Параметры движения
        [SerializeField] private float shakeStrength = 0.2f; // насколько сильно копаются
        [SerializeField] private int vibrato = 10; // количество колебаний
        [SerializeField] private float duration = 2f; // общая длительность
        [SerializeField] private bool randomness = true;

        public void Init(List<GladiatorView> gladiators)
        {
            this.gladiators = gladiators;
        }

        public void PlaceInCircle(ArenaView arena)
        {
            int total = gladiators.Count;

            for (int i = 0; i < total; i++)
            {
                var g = gladiators[i];
                g.transform.position = arena.GetStartPosition();

                g.FaceCenter(arena.CenterPoint);
            }
        }

        public IEnumerator FidgetInCenter()
        {
            List<Tween> activeTweens = new List<Tween>();

            foreach (var g in gladiators)
            {
                // делаем лёгкое хаотичное трясение
                Tween t = g.transform.DOShakePosition(duration, shakeStrength, vibrato, 90, false, false);
                activeTweens.Add(t);
            }

            // ждём пока все завершатся
            foreach (var t in activeTweens)
                yield return t.WaitForCompletion();
        }

        public IEnumerator JumpAllToCenter()
        {
            var sequence = DOTween.Sequence();

            foreach (var g in gladiators)
                sequence.Join(g.JumpToCenter(Vector3.zero));

            yield return sequence.WaitForCompletion();
        }

        public IEnumerable<GladiatorView> GetLosers(string winnerId)
        {
            return gladiators.Where(g => g.Id != winnerId);
        }

        public IEnumerator PlayVictory(string winnerId)
        {
            var winner = gladiators.First(g => g.Id == winnerId);
            winner.PlayVictory();
            yield return new WaitForSeconds(1f);
        }
    }

}
