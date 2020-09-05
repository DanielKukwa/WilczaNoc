using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform target;
    [SerializeField] float rotationSpeed = 5f;
    CharacterCombat combat;
    NavMeshAgent agent;

    public bool SecondAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        if (target != null && SecondAttack)
        {
            agent.SetDestination(target.position);
            FaceTarget(); 
        }
    }

  

    public void MoveToPoint(Vector3 point)
    {
        if(agent.enabled)
            agent.SetDestination(point);

    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;
        target = newTarget.transform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;

    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

    }

    public void FaceTargetImmediately(Transform targetToFace)
    {
        Vector3 direction = (targetToFace.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = lookRotation;
    }

    public void SetTarget(Transform target)
    {
            this.target = target;
    }
}
