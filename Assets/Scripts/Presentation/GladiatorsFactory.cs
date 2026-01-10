using System.Collections.Generic;
using Presentation.Data;
using UnityEngine;

namespace Presentation
{
    public class GladiatorsFactory
    {
        private readonly GladiatorView _prefab;
        private readonly Transform _parent;

        public GladiatorsFactory(GladiatorView prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public List<GladiatorView> Create(List<GladiatorData> gladiatorsData)
        {
            var result = new List<GladiatorView>();

            foreach (var data in gladiatorsData)
            {
                var view = Object.Instantiate(_prefab, _parent);
                view.Init(data); // id, цвет, модель
                result.Add(view);
            }

            return result;
        }
    }
}
