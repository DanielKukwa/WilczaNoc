using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerCutscene : Cutscene
{
    [SerializeField] SelectecedInfo _selectedInfo;
    [SerializeField] SelectecedInfo _selectedInfoSecond;
    [SerializeField] OutlineController _outController;
    [SerializeField] Transform _destination;
    [SerializeField] private float _destinationDistanceOffset = 2f;
    [SerializeField] Transform _lookAt;
    private AudioSource _audio;
    [SerializeField] private float _audioTime = 2f;
    [SerializeField] private GameObject _alternativeEnding;
    [SerializeField] private EatableCorpse _eatableCorpse;
    [SerializeField] private EatEvent _eatEvent;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _audio = GetComponent<AudioSource>();
        _selectedInfo.OnClick += StartEvent;
    }

    protected override void StartEvent()
    {
        base.StartEvent();
        StartCoroutine(GoToPoint());
        _outController.HideOutline();
    }

    IEnumerator GoToPoint()
    {
        _playerAgent.SetDestination(_destination.position);
        StartCoroutine(LookAt(_destination));
        while (Vector3.Distance(_playerAgent.transform.position, _destination.position) > _destinationDistanceOffset)
        {
            yield return null;
        }
        StartCoroutine(LookAt(_lookAt));
        _audio.Play();

        yield return new WaitForSeconds(_audioTime);
        Final();
    }

    protected override void Final()
    {
        _playerAgent.SetDestination(_destination.position);
        _alternativeEnding.gameObject.SetActive(true);
        _outController.SetSelectedInfo(_selectedInfoSecond);
        _eatableCorpse.enabled = true;
        _eatEvent.enabled = true;
        base.Final();
    }
}
