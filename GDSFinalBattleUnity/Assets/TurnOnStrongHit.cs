using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnStrongHit : MonoBehaviour
{
    EventTrigger _eventTrigger;
    // Start is called before the first frame update
    void Start()
    {
        _eventTrigger = GetComponent<EventTrigger>();
        _eventTrigger.OnEventTrigger += OnTriggerActive;
    }

    void OnTriggerActive()
    {
        PlayerManager.instance.player.GetComponent<PlayerController>().HeavySlashEnabled = true;
    }
}
