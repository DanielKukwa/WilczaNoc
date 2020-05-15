using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIndicator : MonoBehaviour
{

    private MouseDetector _mouseDetector;

    private void Start()
    {
        Cursor.visible = false;
        _mouseDetector = FindObjectOfType<MouseDetector>();
        _mouseDetector.OnMouseMove += UpdatePosition;
    }

    private void UpdatePosition(Vector3 position,Vector3 normal)
    {
        Vector3 newPosition = new Vector3(position.x, position.y, position.z);
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(normal);
        
    }

    private void OnDestroy()
    {
        if (_mouseDetector) _mouseDetector.OnMouseMove -= UpdatePosition;
    }
}
