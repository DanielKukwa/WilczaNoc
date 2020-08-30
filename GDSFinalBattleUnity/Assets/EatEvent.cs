using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatEvent : MonoBehaviour
{
    private EatableCorpse _eatableCorpse;
    private bool _eated = false;
    private AudioSource _audio;

    private void Start()
    {
        _eatableCorpse = GetComponent<EatableCorpse>();
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(_eatableCorpse == null &&  _eated == false)
        {
            _eated = true;
            _audio.Play();
        }
    }
}
