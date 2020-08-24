using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneBabcia : Cutscene
{
    private GameObject _light;
    [SerializeField] private Transform _destination;
    [SerializeField] private Transform _doors;
    [SerializeField] private Destructables[] _barricades;
    [SerializeField] private GameObject _leaveTrigger;

    [SerializeField] private float _destinationDistanceOffset = 2f;

    [SerializeField] EventTrigger _eventTrigger;

    private AudioSource _audio;
    [SerializeField] private float _audioTime = 2f;

    protected override void Start()
    {
        base.Start();
        _audio = GetComponent<AudioSource>();
        _eventTrigger.OnEventTrigger += StartEvent;
        _eventTrigger.gameObject.SetActive(false);

        foreach(Destructables d in _barricades)
        {
            d.enabled = false;
        }
        _leaveTrigger.gameObject.SetActive(false);
    }

    protected override void StartEvent()
    {
        _play = true;
        base.StartEvent();
        StartCoroutine(GoToPoint());
    }

    private IEnumerator GoToPoint()
    {
        _playerAgent.SetDestination(_destination.position);
        StartCoroutine(LookAt(_destination));
        while (Vector3.Distance(_playerAgent.transform.position, _destination.position) > _destinationDistanceOffset)
        {
            yield return null;
        }
        StartCoroutine(LookAt(_doors.transform));
        // gadka szmatka babci
        _audio.Play();

        yield return new WaitForSeconds(_audioTime);

        Final();
    }

    protected override void Final()
    {
        _playerAgent.SetDestination(_destination.position);
        // włączyć destructables na barykadzie
        foreach (Destructables d in _barricades)
        {
            d.enabled = true;
        }
        // i trigger końcowy
        _leaveTrigger.gameObject.SetActive(true);

        base.Final();
    }


}
