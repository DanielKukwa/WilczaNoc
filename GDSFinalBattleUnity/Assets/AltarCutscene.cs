using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarCutscene : Cutscene
{
    private EventTrigger _eventTrigger;

    private AudioSource _audio;
    [SerializeField] private float _audioTime = 2f;
    [SerializeField] private Transform _destination;
    [SerializeField] private float _destinationDistanceOffset = 2f;
    [SerializeField] private GameObject _lookAt;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _audio = GetComponent<AudioSource>();
        _eventTrigger = GetComponentInChildren<EventTrigger>();
        _eventTrigger.OnEventTrigger += OnTriggerActive;
    }

    protected override void StartEvent()
    {
        base.StartEvent();
        StartCoroutine(GoToPoint());

    }
    private void OnTriggerActive()
    {
        StartEvent();
    }

    IEnumerator GoToPoint()
    {
        _playerAgent.SetDestination(_destination.position);
        StartCoroutine(LookAt(_destination));
        while (Vector3.Distance(_playerAgent.transform.position, _destination.position) > _destinationDistanceOffset)
        {
            yield return null;
        }
        StartCoroutine(LookAt(_lookAt.transform));
        _audio.Play();
        yield return new WaitForSeconds(_audioTime);
        Final();
    }

    protected override void Final()
    {
        _playerAgent.SetDestination(_destination.position);
        base.Final();
    }
}
