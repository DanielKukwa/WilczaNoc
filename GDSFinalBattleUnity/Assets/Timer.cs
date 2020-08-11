using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float _timeElapsed;
    Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeElapsed += Time.deltaTime;
        int seconds = Mathf.FloorToInt(_timeElapsed % 60);
        int minutes = Mathf.FloorToInt(_timeElapsed / 60);
        _text.text = minutes + ":" + seconds;
    }
}
