using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Presentation
{
    public class ArenaView : MonoBehaviour
    {
        [SerializeField] private RectTransform centerPoint;
        [SerializeField] private Transform[] startPoints;
        [SerializeField] private Transform[] outPoints;
        [SerializeField] private float startRadius = 5f;

        private List<Transform> _useOuts = new List<Transform>();
        private List<Transform> _useStarts = new List<Transform>();

        public Vector3 CenterPoint => centerPoint.position;

        public Vector2 CenterAnchoredPosition =>
            centerPoint.anchoredPosition;

        public Vector3 GetRandomOutPoint()
        {
            if (outPoints.Length == _useOuts.Count)
            {
                _useOuts.Clear();
            }

            var items = outPoints.Except(_useOuts).ToArray();

            var vector = items[Random.Range(0, items.Length)];

            _useOuts.Add(vector); 
            return vector.position;
        }

        public Vector3 GetStartPosition()
        {
            if (startPoints.Length == _useStarts.Count)
            {
                _useStarts.Clear();
            }

            var items = startPoints.Except(_useStarts).ToArray();

            var vector = items[Random.Range(0, items.Length)];

            _useStarts.Add(vector);
            return vector.position;
        }
    }
}
