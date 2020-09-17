using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    private EventTrigger[] _triggers;

    private AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _triggers = GetComponentsInChildren<EventTrigger>();
        
        foreach(EventTrigger trigger in _triggers)
        {
            trigger.OnEventTrigger += PlaySound;
        }

        _audio = GetComponent<AudioSource>();
    }

    private void PlaySound()
    {
        foreach (EventTrigger trigger in _triggers)
        {
            if (trigger)
            {
                Destroy(trigger.gameObject);
            }
        }

        _audio.Play();
    }
}
