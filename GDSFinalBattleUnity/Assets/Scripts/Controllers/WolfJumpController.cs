using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfJumpController : MonoBehaviour
{
    public float lookRadius = 10f;

    public Transform target;
    NavMeshAgent agent;

    private WolfAnimator _wolfAnimator;

    private bool _isJump = false;
    [SerializeField] private float _jumpRadius = 5f;
    [SerializeField] private float _markingTime = 2f;
    [SerializeField] private float _jumpingTime = 1f;
    [SerializeField] private float _restingTime = 2f;




    // TODO use it to enemy attack
    CharacterCombat combat;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        _wolfAnimator = GetComponent<WolfAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);


        if (distance <= lookRadius)
        {
            if (distance <= _jumpRadius && !_isJump)
            {          
               StartCoroutine(Jump());
            }

            if (!_isJump)
            {
                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance)
                {
                    CharacterStats targetStats = target.GetComponent<CharacterStats>();
                    if (targetStats != null)
                    {
                        combat.Attack(targetStats);
                    }
                    FaceTarget();
                }
            }
                     
        }

    }

    IEnumerator Jump()
    {
        _isJump = true;
        agent.enabled = false;
        _wolfAnimator.Animator.SetTrigger("Mark");
        yield return new WaitForSeconds(_markingTime);
        _wolfAnimator.Animator.SetTrigger("Jump");
        float time = 0f;
        Vector3 startPosition = transform.position;
        Vector3 direction = target.position - transform.position;
        Vector3 targetPosition = transform.position + direction * _jumpRadius;
        while(time < _jumpingTime)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / _jumpingTime);
            yield return null;
        }

        _isJump = false;

        yield return new WaitForSeconds(_restingTime);
        
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
