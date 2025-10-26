using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    [CreateAssetMenu(menuName = "GAS/AttributeSet", fileName = "AttributeSet")]
    public class AttributeSet : ScriptableObject
    {
        public List<Attribute> attributes = new();

        public Dictionary<string, Attribute> InstantiateDict()
        {
            var dictionary = new Dictionary<string, Attribute>(StringComparer.OrdinalIgnoreCase);
            foreach (var attribute in attributes)
            {
                dictionary[attribute.Name] =
                    new Attribute(attribute.Name, attribute.CurrentValue, attribute.Min, attribute.Max);
            }
            return dictionary;
        }
    }
}