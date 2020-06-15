using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowlArea : MonoBehaviour
{
    private AudioSource _audio;
    private bool played = false;
    private MeshRenderer _mesh;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _mesh = GetComponent<MeshRenderer>();
        _mesh.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            if(!played)
            {
                _audio.Play();
                played = true;              
            }
            
        }
    }

}
