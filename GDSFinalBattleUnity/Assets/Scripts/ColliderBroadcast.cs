using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderBroadcast : MonoBehaviour
{
    public event Action<Collider> OnEnterTrigger;

    private void OnTriggerEnter(Collider other)
    {
        OnEnterTrigger?.Invoke(other);
    }
}
