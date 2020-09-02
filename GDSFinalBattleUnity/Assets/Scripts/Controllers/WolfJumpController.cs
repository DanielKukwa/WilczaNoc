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

    [SerializeField] private float _attackDistance = 1f;
    

    private bool _isMark = false;
    private bool _isJump = false;
    private bool _isPathBlocked = false;
    [SerializeField] private float _jumpRate = 5f;
    // empty GO to store spot when in finding spot mode
    private GameObject _newSpot;
    [SerializeField] private float _findingSpotTime = 2f;
    [SerializeField] private int _minFindSpotAreaSize = 7;
    [SerializeField] private int _maxFindSpotAreaSize = 10;
    private float _jumpCooldown = 0f;
    [SerializeField] private float _jumpRadiusTrigger = 5f;
    [SerializeField] private float _landingDistanceBehindPlayer = 2f;
    [SerializeField] private float _markingTime = 2f;
    [SerializeField] private float _jumpingTime = 1f;
    [SerializeField] private float _restingTime = 2f;
    [SerializeField] private AnimationCurve _animationCurve;
    private float _correctAnimationTime = 0f;

    [SerializeField] private Slider _slider;
    [SerializeField] private Image _arrowImage;


    // TODO use it to enemy attack
    CharacterCombat combat;

    void Start()
    {
        _newSpot = new GameObject("Spot");
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = _jumpRadiusTrigger;
        combat = GetComponent<CharacterCombat>();
        _wolfAnimator = GetComponent<WolfAnimator>();
        _arrowImage.fillAmount = 0f;
        _slider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);


        if (distance <= lookRadius)
        {
            if (distance <= _jumpRadiusTrigger && !_isJump && _jumpCooldown < Time.time)
            {      
               _jumpCooldown = Time.time + _jumpRate;
               StartCoroutine(Jump());
            }
            else if (!_isJump)
            {
                agent.SetDestination(target.position);
                FaceTarget();

            }


            if (distance <= _attackDistance && _isJump)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                }
            }
            else if(distance <= _attackDistance + 0.75f)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                }
            }
           

            if (_isMark)
            {
                //FaceTarget();
            }
                     
        }

    }

    IEnumerator Jump()
    {
        Vector3 startPosition = transform.position;
        //Vector3 direction = target.position - transform.position;
        Vector3 targetPosition = transform.position + transform.forward *( _jumpRadiusTrigger + _landingDistanceBehindPlayer);
        //NavMeshPath path = new NavMeshPath();
        

        _isPathBlocked = false;

        Vector3 direction = targetPosition - startPosition;

        NavMeshHit hit;

        if(agent.Raycast(targetPosition, out hit))
        {
            GameObject newLine = new GameObject("Line");
            LineRenderer line = newLine.AddComponent<LineRenderer>();
            line.widthMultiplier = 0.1f;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hit.position);
            _isPathBlocked = true;
            _isJump = false;
            agent.enabled = true;
            StartCoroutine(FindSpot());
            StopCoroutine(Jump());          
        }
       //else
       //{
       //    GameObject newLine = new GameObject("Line");
       //    LineRenderer line = newLine.AddComponent<LineRenderer>();
       //    line.widthMultiplier = 0.1f;
       //    line.SetPosition(0, transform.position);
       //    line.SetPosition(1, hit.position);
       //}
        /* for(int i = 1; i <= direction.magnitude; i++)
         {
             Vector3 pathVector = direction.normalized * i ;
             agent.CalculatePath(pathVector, path);
             Debug.Log("Lenght of path: " + pathVector.magnitude);
             if (path.status == NavMeshPathStatus.PathPartial)
             {
                 Debug.Log("Blocked path at: " + pathVector.magnitude);
                 GameObject newLine = new GameObject("Line");
                 LineRenderer line = newLine.AddComponent<LineRenderer>();
                 //line.positionCount = 2;
                 line.widthMultiplier = 0.2f;
                 line.SetPosition(0, transform.position);
                 line.SetPosition(1, transform.position + pathVector);
                 _isPathBlocked = true;
                 _isJump = false;
                 agent.enabled = true;
                 StopCoroutine(Jump());
                 break;
             }
         }*/



        if (!_isPathBlocked)
        {
            _isJump = true;
            _isMark = true;
            agent.enabled = false;
            _wolfAnimator.Animator.SetTrigger("Mark");
            float _timeMarked = 0f;
            _slider.gameObject.SetActive(true);

            while (_timeMarked <= _markingTime)
            {
                _timeMarked += Time.deltaTime;
                _arrowImage.fillAmount = _timeMarked / _markingTime;
                yield return null;
            }

            _arrowImage.fillAmount = 0f;
            _slider.gameObject.SetActive(false);
            _isMark = false;
            _wolfAnimator.Animator.SetTrigger("Jump");

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
            _wolfAnimator.Animator.SetTrigger("BattlePose");
            _isJump = false;
            agent.enabled = true;
        }
        

    }

    private IEnumerator FindSpot()
    {
        _isJump = true;
        int spotDistance = _maxFindSpotAreaSize - _minFindSpotAreaSize;
        int randSize = Random.Range(0, spotDistance);
        int randX = Random.Range(-1, 1);
        if(randX < 0)
        {
            randX = -_minFindSpotAreaSize - randSize;
        }
        else
        {
            randX = _minFindSpotAreaSize + randSize;
        }
        int randZ = Random.Range(-1, 1);
        if (randZ < 0)
        {
            randZ = -_minFindSpotAreaSize - randSize;
        }
        else
        {
            randZ = _minFindSpotAreaSize + randSize;
        }

        Vector3 newPosition = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);
       // Vector3 newPosition = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z + 10);
        _newSpot.transform.position = newPosition;
        agent.SetDestination(newPosition);
        float oldStop = agent.stoppingDistance;
        agent.stoppingDistance = 1f;
        yield return new WaitForSeconds(_findingSpotTime);
        _isJump = false;
        agent.stoppingDistance = oldStop;
        //target = PlayerManager.instance.player.transform;
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
