using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowlIcon : MonoBehaviour
{
    private Slider _slider;

    [SerializeField] private GameObject _target;

    [SerializeField] private Vector3 _offsetPosition;

    public Animator AnimationIcon;

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
        AnimationIcon = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_target)
        {
            Vector3 targetPos = Camera.main.WorldToScreenPoint(_target.transform.position);
            transform.position = targetPos + _offsetPosition;
        }
        
    }

    public void SetSliderMaxValue(float value)
    {
        _slider.maxValue = value;
        _slider.value = value;
    }

    public void UpdateIndicator(float value)
    {
        _slider.value = value;
    }

    public void ChangePosition(Vector3 newPosition)
    {
        transform.position = newPosition + _offsetPosition;
    }

    public void SetOffset(Vector3 offset)
    {
        _offsetPosition = offset;
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    private void OnDisable()
    {
        _target = null;
    }
}
