using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    private CameraController _cameraController;

    [SerializeField] private float _magnitude = 1f;
    [SerializeField] private float _duration = 1f;

    private void Awake()
    {
        if (Instance != null) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        _cameraController = GetComponent<CameraController>();
    }

    public void ShakeCamera()
    {
        StartCoroutine(ChangeCameraPosition(_magnitude, _duration));
    }

    IEnumerator ChangeCameraPosition(float magnitude, float duration)
    {
        float stopTime = Time.time + duration;

        Vector3 cameraPostion = _cameraController.offset;

        while (stopTime > Time.time)
        {
            float randX = Random.Range(-magnitude, magnitude);
            float randZ = Random.Range(-magnitude, magnitude);
            _cameraController.offset = new Vector3(cameraPostion.x + randX, cameraPostion.y, cameraPostion.z + randZ);
            yield return null;
        }

        _cameraController.offset = cameraPostion;
    }
}
