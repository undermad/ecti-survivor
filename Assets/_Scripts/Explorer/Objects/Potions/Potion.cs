using Explorer._Scripts.Explorer.Systems.CombatSystem;
using KBCore.Refs;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Objects.Potions
{
    public class Potion : MonoBehaviour
    {
        [SerializeField, Anywhere] private GameplayAbility gameplayAbility;


        private void OnTriggerEnter2D(Collider2D other)
        {
            var ascOther = other.GetComponent<AbilitySystemComponent>();

            if (ascOther != null)
            {
                gameplayAbility.Activate(ascOther, other.gameObject);
                Destroy(gameObject);
            }
            
        }


    }
}