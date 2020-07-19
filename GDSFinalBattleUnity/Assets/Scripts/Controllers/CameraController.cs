using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float currentZoom = 10f;
    public float pitch = 2f;
    public float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        SlowDownCam();
    }

    public void SlowDownCam()
    {
        Vector3 desiredPosition = target.position - offset * currentZoom;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = target.position - offset * currentZoom;
        //transform.position = smoothedPosition;

        transform.LookAt(target.position + Vector3.up * pitch);
    }
}
