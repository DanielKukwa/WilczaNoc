﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForesterCutscene : Cutscene
{
    private SelectecedInfo _selectecedInfo;
    private OutlineController _outlineController;
    private OutlineVisibility _outlineVisibility;
    [SerializeField] Animator[] _wolfAnimators;

    [SerializeField] LookRadiusTrigger _trigger;
    private Forester _forester;
    [SerializeField] GameObject[] _wolves;
    private bool _wolfStopEat = false;

    [SerializeField] private Transform _destination;
    [SerializeField] private float _destinationDistanceOffset;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _audioTime = 10f;

    [SerializeField] private GameObject _alternativeEnding;

    protected override void Start()
    {
        base.Start();

        _forester = GameObject.FindGameObjectWithTag("Forester").GetComponent<Forester>();

        StartCoroutine(SubscribeSelectedInfo());
        _outlineController = _forester.GetComponent<OutlineController>();
        _outlineVisibility = _forester.GetComponentInChildren<OutlineVisibility>();
        StartCoroutine(DeactivateForesterOutline());

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

        if (!_play)
        {
            if (!_wolves[0] && !_wolves[1])
            {
                if (!_selectecedInfo.IsActive())
                {
                    _outlineController.enabled = true;
                    _outlineVisibility.enabled = true;

                    _selectecedInfo.Activate();
                }
                
            }
        }
        else
        {
            base.Update();
        }      
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
        _alternativeEnding.gameObject.SetActive(true);
        base.Final();
    }

    private IEnumerator DeactivateForesterOutline()
    {
        yield return new WaitForSeconds(2f);
        _outlineController.enabled = false;
        _outlineVisibility.enabled = false;

    }

    private IEnumerator SubscribeSelectedInfo()
    {
        while (!_selectecedInfo)
        {
            _selectecedInfo = _forester.GetComponentInChildren<SelectecedInfo>();
           
            yield return null;
        }

        _selectecedInfo.OnClick += StartEvent;
        _selectecedInfo.Deactivate();
    }
}
