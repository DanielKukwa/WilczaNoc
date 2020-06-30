using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfHowlController : MonoBehaviour
{
    private EventTrigger _eventTrigger;

    public float lookRadius = 10f;

    public Transform target;
    UnityEngine.AI.NavMeshAgent agent;

    // TODO use it to enemy attack
    CharacterCombat combat;

    private WolfAnimator _wolfAnimator;
    private HowlIndicator _howlIndicator;
    [SerializeField] private float _howlAnimationTime = 2f;
    [SerializeField] private float _howlRate = 7f;
    private float _howlCooldown = 0f;
    private bool _isHowl = false;
    private float _howlTimeRemaining = 0f;
    [SerializeField] private AudioSource _audioHowling;

    void Start()
    {
        _howlIndicator = GetComponentInChildren<HowlIndicator>();
        _howlIndicator.SetSliderMaxValue(_howlAnimationTime);

        _eventTrigger = GetComponent<EventTrigger>();

        _wolfAnimator = GetComponent<WolfAnimator>();
        _wolfAnimator.SetHowlAnimationLenght(1 / _howlAnimationTime);

        target = PlayerManager.instance.player.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            if(_howlCooldown < Time.time)
            {
                _isHowl = true;
                _howlTimeRemaining = _howlAnimationTime;
                FaceTarget();
                _howlCooldown = Time.time + _howlRate;
                _wolfAnimator.OnHowl();
                _audioHowling.Play();
            }
            else if(!_isHowl)
            {
                FaceOpposite();
            }          

        }
        else
        {
            FaceTarget();
        }

        if (_isHowl)
        {
            _howlTimeRemaining -= Time.deltaTime;
            _howlIndicator.UpdateIndicator(_howlTimeRemaining);

            if (_howlTimeRemaining <= 0)
            {
                OnHowlEnded();
                _howlIndicator.UpdateIndicator(_howlAnimationTime);
                _isHowl = false;
            }
        }
    }


    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        agent.SetDestination(transform.position);

    }

    void FaceOpposite()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction = - direction;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        agent.SetDestination(transform.position + direction * lookRadius);
    }

    public void OnHowlEnded()
    {
        if (_eventTrigger)
        {
            _eventTrigger.Triggered();           
        }
        else
        {
            Debug.Log("BRAK TRIGGERA!");
        }

        AudioManager.Instance.PlayWolfPack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
