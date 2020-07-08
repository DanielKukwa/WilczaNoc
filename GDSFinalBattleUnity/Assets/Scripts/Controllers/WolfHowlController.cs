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
    private HowlIcon _howlIcon;
    //private HowlIndicator _howlIndicator;
    [SerializeField] private float _howlAnimationTime = 2f;   
    [SerializeField] private float _howlRate = 7f;
    [SerializeField] private float _howlAnimationOffset = 0.25f;
    private float _howlCooldown = 0f;
    private bool _isHowl = false;
    private float _howlTimeRemaining = 0f;
    private AudioSource _audioHowling;

    void Start()
    {
        //_howlIndicator = GetComponentInChildren<HowlIndicator>();
        //_howlIndicator.SetSliderMaxValue(_howlAnimationTime - _howlAnimationOffset);

        _eventTrigger = GetComponent<EventTrigger>();

        _wolfAnimator = GetComponent<WolfAnimator>();
        _wolfAnimator.SetHowlAnimationLenght(1 / _howlAnimationTime);

        _audioHowling = GetComponent<AudioSource>();

        target = PlayerManager.instance.player.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();

        CharacterStats stats = GetComponent<CharacterStats>();
        if (!stats) Debug.Log("Brak character stats");
        stats.OnCharacterDie += OnHowlWolfDead;
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
                
                FaceTarget();
                _howlCooldown = Time.time + _howlRate;

                _howlTimeRemaining = _howlAnimationTime - _howlAnimationOffset;

                if (_howlIcon == null)
                {
                    _howlIcon = HowlIconPool.Instance.Get();
                    GameObject canvas = GameObject.FindGameObjectWithTag("DirUI");
                    if (!canvas) Debug.Log("brakCanvsu");
                    _howlIcon.transform.SetParent(canvas.transform);
                    _howlIcon.gameObject.SetActive(true);
                    _howlIcon.SetTarget(this.gameObject);
                    _howlIcon.SetSliderMaxValue(_howlAnimationTime - _howlAnimationOffset);
                }

                _howlIcon.UpdateIndicator(_howlTimeRemaining);
                _howlIcon.AnimationIcon.SetTrigger("Howl_Load");
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

            if (_howlIcon)
            {
                _howlIcon.UpdateIndicator(_howlTimeRemaining);
                if (_howlTimeRemaining <= 0)
                {
                    OnHowlEnded();
                    _howlIcon.AnimationIcon.SetTrigger("Howl_Icon");
                    _isHowl = false;
                }
            }
            else
            {
                Debug.Log("Brak howl icon");
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

    private void OnHowlWolfDead()
    {
        HowlIconPool.Instance.ReturnToPool(_howlIcon);
        _howlIcon = null;
    }
}
