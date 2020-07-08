using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirIcon : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _angleOffset = 45f;
    [SerializeField] private float _iconOffset = 50f;
    [SerializeField] private GameObject _arrowTransform;

    void Update()
    {
        if (_target)
        {
            Vector3 targetPos = Camera.main.WorldToScreenPoint(_target.transform.position);
            if (targetPos.x > 0 + _iconOffset && targetPos.x < Screen.width - _iconOffset && targetPos.y > 0 + _iconOffset && targetPos.y < Screen.height - _iconOffset) DirectionIconPool.Instance.ReturnToPool(this);
            CalculateIconPositionOnScreen(targetPos);
            RotateIcon(targetPos);
        }
        else
        {
            Debug.Log(gameObject.name + ": brak targetu!");
        } 
    }

    private void CalculateIconPositionOnScreen(Vector3 targetOnScreen)
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 targetPos = targetOnScreen;

        Vector3 direction = targetPos - screenCenter;

        float slope = direction.y / direction.x;
        float x = 0;
        float y = 0;

        if(direction.y < 0)
        {
            x = (-screenCenter.y + _iconOffset) / slope;
            y = -screenCenter.y + _iconOffset;
        }
        else
        {
            x = (screenCenter.y - _iconOffset) / slope;
            y = screenCenter.y - _iconOffset;
        }

        if (x > screenCenter.x - _iconOffset)
        {
            x = screenCenter.x - _iconOffset;
            y = slope * x;         
        }
        else if(x < - screenCenter.x + _iconOffset)
        {
            x = -screenCenter.x + _iconOffset;
            y = slope * x;
        }

        
        Vector3 position = new Vector3(screenCenter.x + x, screenCenter.y + y, 0);
        transform.position = position;

        
    }

    private void RotateIcon(Vector3 targetPos)
    {
        Vector3 iconCenter = transform.position;
        Vector3 direction = targetPos - iconCenter;

        float angle = Mathf.Atan2(direction.y, direction.x);
        angle = angle * 180 / Mathf.PI;

        _arrowTransform.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - _angleOffset));
    }   

    public void SetTarget(GameObject target)
    {
        _target = target;
    }
}
