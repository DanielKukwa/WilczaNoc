using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _endingGameObject;
    private EventTrigger[] _eventTriggers;

    // Start is called before the first frame update
    void Start()
    {
        _eventTriggers = GetComponentsInChildren<EventTrigger>();

        foreach (EventTrigger e in _eventTriggers)
        {
            e.OnEventTrigger += OnTriggerActive;
        }
    }

    private void OnTriggerActive()
    {
        _endingGameObject.SetActive(true);
    }
}
