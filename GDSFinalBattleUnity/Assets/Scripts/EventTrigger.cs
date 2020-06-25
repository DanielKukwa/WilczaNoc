
using UnityEngine;
using System;

public class EventTrigger : MonoBehaviour
{
    public event Action OnEventTrigger;

    public void Triggered()
    {
        OnEventTrigger?.Invoke();
    }
}
