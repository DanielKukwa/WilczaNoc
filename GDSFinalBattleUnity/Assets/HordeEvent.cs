using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeEvent : MonoBehaviour
{
    private EventTrigger _eventTrigger;
    private AudioSource _audio;
    [SerializeField] private EventTrigger[] _eventTriggers;
    [SerializeField] private float _audioTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _eventTrigger = GetComponent<EventTrigger>();

        foreach (EventTrigger e in _eventTriggers)
        {
            e.OnEventTrigger += OnTriggerActive;
        }
    }

    private void OnTriggerActive()
    {
        StartCoroutine(HordeAttackDelay());
    }

    private IEnumerator HordeAttackDelay()
    {
        _audio.Play();
        yield return new WaitForSeconds(_audioTime);
        _eventTrigger.Triggered();
        
    }
}
