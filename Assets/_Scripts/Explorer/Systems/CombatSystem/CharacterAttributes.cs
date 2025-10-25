using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    [CreateAssetMenu(menuName = "CombatSystem/Attributes")]
    public class CharacterAttributes : ScriptableObject
    {
        public int currentHealthPoints;
        public int maxHealthPoints;
    }
}