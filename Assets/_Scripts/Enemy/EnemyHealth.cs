using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyStatsSO enemyStatsSO;
    [SerializeField] private float currentHealth;

    public event Action Damaged;

    private void Start()
    {
        currentHealth = enemyStatsSO.health;
    }

    public void TakeDamage(float damage)
    {
        Damaged?.Invoke();
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