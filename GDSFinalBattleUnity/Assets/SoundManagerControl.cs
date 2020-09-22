using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerControl : MonoBehaviour
{
    AudioSource audio;
    public AudioClip dashSound;


    void Start()
    {
        audio = GetComponent<AudioSource>();    
    }

    public void PlayDashSound()
    {
        audio.PlayOneShot(dashSound);
    }
}
