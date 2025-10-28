using UnityEngine;
using UnityEngine.Serialization;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    [CreateAssetMenu(menuName = "GAS/AttributeNameData", fileName = "AttributeNameData")]
    public class AttributeNameData : ScriptableObject
    {
        [SerializeField] private string attributeName;
        public string Value => attributeName;
    }
}