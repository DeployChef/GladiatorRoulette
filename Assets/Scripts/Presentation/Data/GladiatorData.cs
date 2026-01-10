using UnityEngine;

namespace Presentation.Data
{
    /// <summary>
    /// ScriptableObject для хранения данных одного гладиатора.
    /// Это позволяет создавать гладиаторов в редакторе Unity как ассеты.
    /// </summary>
    [CreateAssetMenu(fileName = "GladiatorData", menuName = "GladiatorRoulette/Gladiator Data", order = 1)]
    public class GladiatorData : ScriptableObject
    {
        [SerializeField] private string gladiatorName = "Gladiator";
        [SerializeField] private Color color = Color.white;
        
        public string Name => gladiatorName;
        public Color Color => color;
        
        /// <summary>
        /// Генерирует уникальный ID на основе имени (можно использовать для связи с Domain.Gladiator)
        /// </summary>
        public string GetId() => $"gladiator_{gladiatorName.Replace(" ", "_")}";
    }
}

