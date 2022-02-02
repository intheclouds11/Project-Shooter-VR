using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyStatsSO enemyStatsSO;
    [SerializeField] public float currentHealth;

    private void Awake()
    {
        currentHealth = enemyStatsSO.health;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Hit! HP remaining: {currentHealth}");

        if (currentHealth <= 0)
        {
            EnemyManager.Instance._currentEnemyList.Remove(this.gameObject);
            if (EnemyManager.Instance._currentEnemyList.Count <= 0)
            {
                GameManager.Instance.UpdateGameState(GameState.Win);
            }
            Destroy(this.gameObject);
        }
    }
}