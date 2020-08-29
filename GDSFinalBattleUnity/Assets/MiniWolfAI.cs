using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniWolfAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private GameObject trigger;
    private bool _triggerActivated = false;
    [SerializeField] private Transform _destination;
    [SerializeField] private float _destinationDistanceOffset = 2f;
    [SerializeField] private GameObject _lookAt;
    private WolfNormalController _wolfNormalController;
    private CapsuleCollider _collider;


    private void Start()
    {
        _wolfNormalController = GetComponent<WolfNormalController>();
        _agent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if(trigger == null && !_triggerActivated)
        {
            _triggerActivated = true;
            StartCoroutine(GoToPoint());
        }
    }

    private IEnumerator GoToPoint()
    {
        _agent.enabled = true;
        _agent.SetDestination(_destination.position);
        while (Vector3.Distance(_agent.transform.position, _destination.position) > _destinationDistanceOffset)
        {
            yield return null;
        }
        StartCoroutine(LookAt(_lookAt.transform));
        _wolfNormalController.enabled = true;
        _wolfNormalController.lookRadius = 5f;
        _collider.enabled = true;
    }

    private IEnumerator LookAt(Transform lookAtTranform)
    {
        Vector3 direction = (lookAtTranform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, elapsedTime);
            yield return null;
        }

    }
}
