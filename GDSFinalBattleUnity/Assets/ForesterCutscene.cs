using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForesterCutscene : Cutscene
{
    [SerializeField] Animator[] _wolfAnimators;

    [SerializeField] LookRadiusTrigger _trigger;
    private Transform _foresterTransform;
    [SerializeField] GameObject[] _wolves;
    private bool _wolfStopEat = false;

    [SerializeField] private Transform _destination;
    [SerializeField] private float _destinationDistanceOffset;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _audioTime = 10f;


    protected override void Start()
    {
        base.Start();

        _foresterTransform = GameObject.FindGameObjectWithTag("Forester").transform;
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
                StartEvent();
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
        base.StartEvent();
        StartCoroutine(GoToPoint());
    }

    IEnumerator GoToPoint()
    {
        _playerAgent.SetDestination(_destination.position);
        _playerController.motor.SetTarget(_destination);
        while (Vector3.Distance(_playerAgent.transform.position, _destination.position) > _destinationDistanceOffset)
        {
            yield return null;
        }
        _playerController.motor.SetTarget(_foresterTransform);
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
