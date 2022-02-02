using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class EnemyAttack : MonoBehaviour
{
    private PlayerHealth _target;
    public EnemyStatsSO enemyStatsSO;
    private float _damage;

    private void Start()
    {
        _target = GetComponent<EnemyAI>()._target;
        _damage = enemyStatsSO.damage;
    }

    public void AttackHitEvent() // this is called in the enemy animator attack event
    {
        if (_target == null)
        {
            Debug.Log("null target during attack!");
            return;
        }

        _target.TakeDamage(_damage);
    }
}