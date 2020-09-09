﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    private Animator _anim;
    private AudioSource _audio;
    [SerializeField] private GameObject[] _lines;
    [SerializeField] private GameObject[] _wolfToDestroy;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_lines[0] && !_lines[1])
        {
            _anim.SetTrigger("Fall");
            _audio.Play();
            foreach(GameObject wolf in _wolfToDestroy)
            {
                Destroy(wolf.gameObject);
            }
            Destroy(this);
        }
    }
}
