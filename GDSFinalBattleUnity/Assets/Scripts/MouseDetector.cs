using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseDetector : MonoBehaviour
{
    public Action<Vector3,Vector3> OnMouseMove;

    [SerializeField] private GameObject _bloodSplash;

    private RaycastHit _hitInfo;
    private Ray _ray;
    [SerializeField] private LayerMask _layer;

    // Update is called once per frame
    void Update()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity, _layer))
        {
            OnMouseMove?.Invoke(_hitInfo.point, _hitInfo.normal);
            Debug.DrawRay(_hitInfo.point, _hitInfo.normal, Color.green);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Instantiate(_bloodSplash, _hitInfo.point, Quaternion.LookRotation(_hitInfo.normal));
            }
        }
    }
}
