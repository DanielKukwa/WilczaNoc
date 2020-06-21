using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NarrativeEvent : MonoBehaviour
{
    [SerializeField] private bool _isTextBased;
    [TextArea] public string _myText;
    [SerializeField] private AudioClip _voiceOver;

    private MeshRenderer _meshRenderer => GetComponentInChildren<MeshRenderer>();
    private ColliderBroadcast _colliderBroadcast => GetComponentInChildren<ColliderBroadcast>();
    private TextMeshProUGUI _text => GetComponentInChildren<TextMeshProUGUI>();
    private AudioSource _audio => GetComponentInChildren<AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        _colliderBroadcast.OnEnterTrigger += TriggerEvent;

        _meshRenderer.enabled = false;

        if (_isTextBased)
        {
            _text.text = _myText;          
        }
        else
        {
            _audio.clip = _voiceOver;
        }

        _text.enabled = false;

    }

    private void TriggerEvent(Collider other)
    {
        if(other.tag == "Player")
        {
            _colliderBroadcast.gameObject.SetActive(false);

            if (_isTextBased)
            {
                _text.enabled = true;
            }
            else
            {
                _audio.Play();
            }
        }
    }

}
