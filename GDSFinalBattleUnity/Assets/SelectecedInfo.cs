using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectecedInfo : MonoBehaviour
{
    public Action OnClick;
    // Update is called once per frame
    void Update()
    {
        if (this.isActiveAndEnabled)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 37, 0));

            if (Input.GetMouseButtonDown(0))
            {
                OnClick?.Invoke();
            }
        }
    }
}
