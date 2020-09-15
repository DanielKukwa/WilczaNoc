using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForesterCutscene : Cutscene
{
    private SelectecedInfo _selectecedInfo;
    [SerializeField] Animator[] _wolfAnimators;

    [SerializeField] LookRadiusTrigger _trigger;
    private Forester _forester;
    [SerializeField] GameObject[] _wolves;
    private bool _wolfStopEat = false;

    [SerializeField] private Transform _destination;
    [SerializeField] private float _destinationDistanceOffset;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _audioTime = 10f;


    protected override void Start()
    {
        base.Start();

        _forester = GameObject.FindGameObjectWithTag("Forester").GetComponent<Forester>();
        _selectecedInfo = _forester.GetComponentInChildren<SelectecedInfo>();

        _selectecedInfo.OnClick += StartEvent;
        if (!_selectecedInfo) Debug.Log("BRAK SELECTED INFO!!!!");
        foreach (Animator anim in _wolfAnimators)
        {
            anim.SetTrigger("Eat");
        }

    }

    protected override void Update()
    {
        if (_trigger == null && !_wolfStopEat)
        {          
            foreach (Animator anim in _wolfAnimators)
            {
                anim.SetTrigger("StopEat");
            }
            _wolfStopEat = true;
        }

        //if (!_play)
        //{
        //    if (!_wolves[0] && !_wolves[1])
        //    {
        //        StartEvent();
        //    }
        //}
        //else
        //{
            base.Update();
       // }      
    }

    protected override void StartEvent()
    {
        _play = true;
        _forester.enabled = true;
        base.StartEvent();
        StartCoroutine(GoToPoint());
    }

    IEnumerator GoToPoint()
    {
        _playerAgent.SetDestination(_destination.position);
        StartCoroutine(LookAt(_destination));
        while (Vector3.Distance(_playerAgent.transform.position, _destination.position) > _destinationDistanceOffset)
        {
            yield return null;
        }
        StartCoroutine(LookAt(_forester.transform));
        _audio.Play();

        yield return new WaitForSeconds(_audioTime);
        Final();
    }

    protected override void Final()
    {
        _playerAgent.SetDestination(_destination.position);
        _forester.Healthbar.gameObject.SetActive(true);
        _forester.Healthbar.SetSliderMaxValue(10);
        _forester.Healthbar.SetSliderValue(1);
        _forester.Healthbar.Animator.SetTrigger("Show");
        base.Final();
    }
}
