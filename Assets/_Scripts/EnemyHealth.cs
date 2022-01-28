using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _hitPoints = 100f;

    public void TakeDamage(float damage)
    {
        _hitPoints -= damage;
        Debug.Log($"Hit! HP remaining: {_hitPoints}");
        
        if (_hitPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}