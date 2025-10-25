using Explorer._Scripts.Explorer.Systems.CombatSystem;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Explorer._Scripts.Explorer.Components.Character
{
    public class CharacterBase : ValidatedMonoBehaviour
    {
        [SerializeField, Anywhere] private CharacterAttributes characterAttributes;
        [FormerlySerializedAs("gameplayAbilitySystem")] [SerializeField, Self] private AbilitySystemComponent abilitySystemComponent;
    }
}