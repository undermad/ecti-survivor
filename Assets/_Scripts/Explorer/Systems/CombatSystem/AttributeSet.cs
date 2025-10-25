using System;
using System.Collections.Generic;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Systems.CombatSystem
{
    [CreateAssetMenu(menuName = "GAS/AttributeSet", fileName = "AttributeSet")]
    public class AttributeSet : ScriptableObject
    {
        public List<Attribute> Attributes = new();

        public Dictionary<string, Attribute> InstantiateDict()
        {
            var dictionary = new Dictionary<string, Attribute>(StringComparer.OrdinalIgnoreCase);
            foreach (var attribute in Attributes)
            {
                dictionary[attribute.Name] =
                    new Attribute(attribute.Name, attribute.BaseValue, attribute.Min, attribute.Max);
            }

            foreach (var attribute in dictionary.Values)
            {
                attribute.ResetToBase();
            }

            return dictionary;
        }
    }
}