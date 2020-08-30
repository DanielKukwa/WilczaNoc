using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePLTextEvent : MonoBehaviour
{
    private EventTrigger _eventTrigger;

    private void Start()
    {
        _eventTrigger = GetComponentInChildren<EventTrigger>();
        _eventTrigger.OnEventTrigger += OnTriggerActive;
    }

    private void OnTriggerActive()
    {
        PlayerTextures.Instance.ChangeHoodTexture();
    }
}
