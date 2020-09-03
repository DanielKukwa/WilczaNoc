using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    private Slider dashSlider;
    //public float cooldown;
        
    // Start is called before the first frame update
    void Awake()
    {
        dashSlider = GetComponentInChildren<Slider>();

    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Euler(new Vector3(0, 37, 0));
        
    }

    public void SetCooldownValue(float value)
    {
        dashSlider.value = value;
    }

    public void SetCooldownMaxValue(float maxValue)
    {
        dashSlider.maxValue = maxValue;
        dashSlider.value = maxValue;
    }
}
