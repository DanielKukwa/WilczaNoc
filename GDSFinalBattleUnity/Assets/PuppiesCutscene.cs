using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppiesCutscene : Cutscene
{
    //private EventTrigger _eventTrigger;
    [SerializeField] Destructables[] _destructables;
    [SerializeField] SelectecedInfo[] _selectecedInfos;
    [SerializeField] SelectecedInfo[] _selectecedInfosSecond;

    private AudioSource _audio;
    [SerializeField] private float _audioTime = 3f;
    [SerializeField] private Transform _destination;
    [SerializeField] private float _destinationDistanceOffset = 2f;
    [SerializeField] private GameObject _lookAt;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _audio = GetComponent<AudioSource>();
        //_eventTrigger = GetComponentInChildren<EventTrigger>();
        //_eventTrigger.OnEventTrigger += OnTriggerActive;
        foreach(SelectecedInfo info in _selectecedInfos)
        {
            info.OnClick += OnTriggerActive;
        }

    }

    protected override void StartEvent()
    {
        for(int i = 0; i <_selectecedInfos.Length; i++)
        {
            OutlineController outlineController = _selectecedInfos[i].transform.parent.GetComponent<OutlineController>();
            outlineController.SetSelectedInfo(_selectecedInfosSecond[i]);
            Destroy(_selectecedInfos[i].gameObject);
        }

        foreach(Destructables d in _destructables)
        {
            d.enabled = true;
        }

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
