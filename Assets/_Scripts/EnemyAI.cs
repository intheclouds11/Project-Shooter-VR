using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange = 5f;

    private NavMeshAgent _mNavMeshAgent;
    private float _distanceToTarget = Mathf.Infinity;
    private bool _isProvoked;

    private Animator _animator;

    private void Awake()
    {
        _mNavMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _distanceToTarget = Vector3.Distance(target.position, this.transform.position);
        if (_isProvoked)
        {
            EngageTarget();
        }
        else if (_distanceToTarget <= chaseRange)
        {
            _isProvoked = true;
        }
    }

    private void EngageTarget()
    {
        if (_distanceToTarget >= _mNavMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (_distanceToTarget <= _mNavMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        _mNavMeshAgent.SetDestination(target.position);
        _animator.SetTrigger("move");
        _animator.SetBool("attack", false);
    }

    private void AttackTarget()
    {
        _animator.SetBool("attack", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}