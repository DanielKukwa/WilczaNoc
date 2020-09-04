using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnDash : MonoBehaviour
{
    private EventTrigger _eventTrigger;

    private void Start()
    {
        _eventTrigger = GetComponent<EventTrigger>();
        _eventTrigger.OnEventTrigger += OnTriggerActive;
    }

    private void OnTriggerActive()
    {
        SpecialAbilities playerAbilities = PlayerManager.instance.player.GetComponent<SpecialAbilities>();
        playerAbilities.DashEnabled = true;
        playerAbilities.dashBar.gameObject.SetActive(true);

    }
}
