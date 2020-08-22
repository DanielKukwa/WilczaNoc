using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneBabcia : Cutscene
{
    private GameObject _light;

    [SerializeField] EventTrigger _eventTrigger;

    protected override void Start()
    {
        base.Start();
        _eventTrigger.OnEventTrigger += StartEvent;
        _eventTrigger.gameObject.SetActive(false);
    }


    
}
