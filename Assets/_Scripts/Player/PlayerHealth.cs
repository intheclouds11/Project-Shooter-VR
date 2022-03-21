using System;
using UnityEngine;
using UnityEngine.UI;

namespace intheclouds
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] public PlayerStatsSO playerStatsSO;
        [SerializeField] public float currentHealth;
        [SerializeField] private Slider healthSlider;
        [SerializeField] public float maxHealth;

        public event Action Damaged; // use for other classes to know when player is damaged
    
        private void Start()
        {
            maxHealth = playerStatsSO.health;
            currentHealth = playerStatsSO.health;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        public void TakeDamage(float damage)
        {
            Damaged?.Invoke();
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            if (currentHealth <= 0)
            {
                GameManager.Instance.UpdateGameState(GameState.Lose);
            }
        }
    }
}