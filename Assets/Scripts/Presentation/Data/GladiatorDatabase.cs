using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Infrastructure.Random;

namespace Presentation.Data
{
    /// <summary>
    /// ScriptableObject база данных всех гладиаторов.
    /// Содержит список всех доступных гладиаторов и может выбирать случайных для боя.
    /// </summary>
    [CreateAssetMenu(fileName = "GladiatorDatabase", menuName = "GladiatorRoulette/Gladiator Database", order = 2)]
    public class GladiatorDatabase : ScriptableObject
    {
        [SerializeField] private List<GladiatorData> allGladiators = new List<GladiatorData>();
        
        public List<GladiatorData> AllGladiators => allGladiators;
    }
}

