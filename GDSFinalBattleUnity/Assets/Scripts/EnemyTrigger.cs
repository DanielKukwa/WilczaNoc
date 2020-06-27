using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTrigger : MonoBehaviour
{
    private EventTrigger _eventTrigger;

    private MeshRenderer _meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
        _eventTrigger = GetComponent<EventTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
           if (_eventTrigger)
            {
                _eventTrigger.Triggered();
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("BRAK TRIGGERA!");
            }
        }
    }
}
