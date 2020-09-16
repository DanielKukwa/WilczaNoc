using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectecedInfo : MonoBehaviour
{
    public Action OnClick;

    private bool _active = false;

    void Update()
    {
        if (this.isActiveAndEnabled && _active)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 37, 0));

            if (Input.GetMouseButtonDown(0))
            {
                OnClick?.Invoke();
                Destroy(this.gameObject);
            }
        }
    }

    public void MoveToCamera()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 37, 0));
    }

    public void Activate()
    {
        _active = true;
    }

    public bool IsActive()
    {
        return _active;
    }
}
