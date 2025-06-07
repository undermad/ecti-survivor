using UnityEngine;
using UnityEngine.UI;

namespace Explorer._Project.Scripts.Enemy
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBarSprite;
        
        public void UpdateHealthBar(float maxHealth, float currentHealth)
        {
            Debug.Log("Max Health: " + maxHealth + " Current Health: " + currentHealth);
            _healthBarSprite.fillAmount = currentHealth / maxHealth;
        }
        
    }
}