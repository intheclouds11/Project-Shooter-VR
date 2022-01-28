using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private float _chaseRange = 5f;

    private NavMeshAgent _mNavMeshAgent;
    private float _distanceToTarget = Mathf.Infinity;
    private bool _isProvoked;

    private Animator _animator;

    private PlayerHealth _targetHealth;

    private void Awake()
    {
        _mNavMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _targetHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        _target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        _distanceToTarget = Vector3.Distance(_target.position, this.transform.position);
        if (_isProvoked)
        {
            EngageTarget();
        }
        else if (_distanceToTarget <= _chaseRange)
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
        _mNavMeshAgent.SetDestination(_target.position);
        _animator.SetTrigger("move");
        _animator.SetBool("attack", false); // stop attack animation when chasing
    }

    private void AttackTarget()
    {
        if (_targetHealth.hitPoints <= 0)
        {
            _animator.SetBool("attack", false);
            return;
        }

        _animator.SetBool("attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (_target.position - this.transform.position).normalized;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
    }
}