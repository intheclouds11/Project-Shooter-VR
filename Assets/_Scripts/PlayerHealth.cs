using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public PlayerStatsSO playerStatsSO;
    [SerializeField] public float currentHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] public float maxHealth;

    private void Awake()
    {
        maxHealth = playerStatsSO.health;
        currentHealth = playerStatsSO.health;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
    }
}