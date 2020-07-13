using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WolfJumpController : MonoBehaviour
{
    public float lookRadius = 10f;

    public Transform target;
    NavMeshAgent agent;

    private WolfAnimator _wolfAnimator;



    private bool _isMark = false;
    private bool _isJump = false;
    [SerializeField] private float _jumpRadius = 5f;
    [SerializeField] private float _markingTime = 2f;
    [SerializeField] private float _jumpingTime = 1f;
    [SerializeField] private float _restingTime = 2f;
    [SerializeField] private AnimationCurve _animationCurve;
    private float _correctAnimationTime = 0f;

    [SerializeField] private Image _arrowImage;


    // TODO use it to enemy attack
    CharacterCombat combat;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        _wolfAnimator = GetComponent<WolfAnimator>();
        _arrowImage.fillAmount = 0f;
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


            if (distance <= agent.stoppingDistance && _isJump)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                }
            }

            if (!_isJump)
            {
                agent.SetDestination(target.position);
                FaceTarget();
               
            }

            if (_isMark)
            {
                FaceTarget();
            }
                     
        }

    }

    IEnumerator Jump()
    {
        _isJump = true;
        _isMark = true;
        agent.enabled = false;
        _wolfAnimator.Animator.SetTrigger("Mark");
        float _timeMarked = 0f;
        while(_timeMarked <= _markingTime)
        {
            _timeMarked += Time.deltaTime;
            _arrowImage.fillAmount = _timeMarked / _markingTime;
            yield return null;
        }
        // yield return new WaitForSeconds(_markingTime);
        _arrowImage.fillAmount = 0f;
        _isMark = false;
        _wolfAnimator.Animator.SetTrigger("Jump");
        
        Vector3 startPosition = transform.position;
        Vector3 direction = target.position - transform.position;
        Vector3 targetPosition = transform.position + direction.normalized * _jumpRadius * 2;

        float elapsedTime = 0f;
        _correctAnimationTime = 0f;
        while (elapsedTime < _jumpingTime)
        {
            elapsedTime += Time.deltaTime;
            float t = _animationCurve.Evaluate(elapsedTime / _jumpingTime);

            transform.position = Vector3.LerpUnclamped(startPosition, targetPosition, t);
            yield return null;
        }

        
        yield return new WaitForSeconds(_restingTime);

        _isJump = false;
        agent.enabled = true;

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
