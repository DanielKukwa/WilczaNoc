using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private Animator _blackScreen;
    private AudioSource _audio;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    private void Start()
    {
        _blackScreen.SetTrigger("FadeOut");
    }
}
