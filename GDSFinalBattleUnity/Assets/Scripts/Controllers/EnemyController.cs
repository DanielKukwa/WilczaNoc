using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    public Transform target;
    NavMeshAgent agent;

    private bool _jump = false;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    [SerializeField] private float _markingTime = 1f;
    [SerializeField] private float _jumpDuration = 1f;
    [SerializeField] private AnimationCurve _jumpCurve;
    private float _elapsedTime = 0;
    [SerializeField] private float _jumpDistance = 1f;
    [SerializeField] private float _restingTime = 2f;


    // TODO use it to enemy attack
    CharacterCombat combat;

    void Start()
    {

        
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            CharacterAnimator characterAnimator = GetComponent<CharacterAnimator>();
            characterAnimator.OnMark();
            _jump = true;
            _startPosition = transform.position;
            _targetPosition = transform.position + transform.forward * _jumpDistance;
            _elapsedTime = 0;
            agent.enabled = false;
            StartCoroutine(Jump());
        }

        if(!_jump)
        {
            float distance = Vector3.Distance(target.position, transform.position);


            if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance)
                {
                    CharacterStats targetStats = target.GetComponent<CharacterStats>();
                    if (targetStats != null)
                    {
                        //combat.Attack(targetStats);
                    }
                    FaceTarget();
                }

            }
        }

        
        
    }


    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(_markingTime);
        CharacterAnimator characterAnimator = GetComponent<CharacterAnimator>();
        characterAnimator.OnMark();
        yield return new WaitForSeconds(_markingTime);

        characterAnimator.OnJump();

        while (_elapsedTime < 1)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / _jumpDuration;
            float curveValue = _jumpCurve.Evaluate(t);
            transform.position = Vector3.LerpUnclamped(_startPosition, _targetPosition, curveValue);
            yield return null;
        }

        yield return new WaitForSeconds(_restingTime);

        agent.enabled = true;
        _jump = false;
    }
        
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
