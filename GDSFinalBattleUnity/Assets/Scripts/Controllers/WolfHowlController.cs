using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfHowlController : MonoBehaviour
{
    public float lookRadius = 10f;

    public Transform target;
    UnityEngine.AI.NavMeshAgent agent;

    // TODO use it to enemy attack
    CharacterCombat combat;

    private WolfAnimator _wolfAnimator;
    [SerializeField] private float _howlRate = 7f;
    private float _howlCooldown = 0f;

    void Start()
    {
        _wolfAnimator = GetComponent<WolfAnimator>();
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
            //agent.SetDestination(target.position);
            if(_howlCooldown < Time.time)
            {
                _howlCooldown = Time.time + _howlRate;
                _wolfAnimator.OnHowl();
                Debug.Log("Howl");
            }

            FaceTarget();
            //if (distance <= agent.stoppingDistance)
            //{
            //    CharacterStats targetStats = target.GetComponent<CharacterStats>();
            //    if (targetStats != null)
            //    {
            //        combat.Attack(targetStats);
            //    }
            //    
            //}

        }

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
