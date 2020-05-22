using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Slider _slider;

    private Vector3 _offsetPosition;

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 37, 0));
    }

    public void SetSliderMaxValue(int value)
    {
        _slider.maxValue = value;
        _slider.value = value;
    }

    public void UpdateHealth(int value)
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
}
