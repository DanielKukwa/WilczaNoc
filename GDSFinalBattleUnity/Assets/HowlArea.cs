using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowlArea : MonoBehaviour
{
    private AudioSource _audio;
    private MeshRenderer _mesh;
    private SphereCollider _collider;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _mesh = GetComponent<MeshRenderer>();
        _mesh.enabled = false;
        _collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            _audio.Play();
            _collider.enabled = false;             

        }
    }

}
