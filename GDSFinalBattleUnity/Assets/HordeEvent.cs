using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeEvent : Cutscene
{
    [SerializeField] private bool _iconBase = false;
    private EventTrigger _eventTrigger;
    private AudioSource _audio;
    [SerializeField] private EventTrigger[] _eventTriggers;
    [SerializeField] private GameObject _spawner;
    [SerializeField] private float _blackScreenTime = 2f;
    [SerializeField] private Animator _blackScreen;
    [SerializeField] private float _audioTime = 2f;
    [SerializeField] private Transform _cameraDestination;
    [SerializeField] private GameObject _camLight;
    [SerializeField] private float _camFlyTime = 2f;
    [SerializeField] private float _camStayTime = 2f;
    [SerializeField] private AudioClip[] _wolfSounds;
    // Start is called before the first frame update
    protected override void Start()
    {
        _spawner.gameObject.SetActive(false);
        _audio = GetComponent<AudioSource>();
        _eventTrigger = GetComponent<EventTrigger>();
        if(_eventTriggers != null)
        {
            foreach (EventTrigger e in _eventTriggers)
            {
                e.OnEventTrigger += OnTriggerActive;
            }
        }
        
        base.Start();
    }

    private void OnTriggerActive()
    {
        StartCoroutine(HordeAttackDelay());
        if (!_iconBase)
        {
            StartEvent();
        }
        
    }

    private IEnumerator HordeAttackDelay()
    {
        _audio.Play();
        if (_iconBase)
        {
            gameObject.tag = "WolfHowl";
            yield return new WaitForSeconds(_audioTime);
            _audio.PlayOneShot(_wolfSounds[0], 0.1f);
            yield return new WaitForSeconds(1f);
            _audio.PlayOneShot(_wolfSounds[1], 0.5f);
            _eventTrigger.Triggered();
            yield return new WaitForSeconds(2f);
            
        }
        else
        {
            _spawner.SetActive(true);
            yield return new WaitForSeconds(_audioTime);
            _blackScreen.Play("BS_fadeout");
            yield return new WaitForSeconds(_blackScreenTime);
            // TRIGGER HORDE
            //_eventTrigger.Triggered();
            

            CameraController camController = Camera.main.GetComponent<CameraController>();
            camController.enabled = false;
            _camLight.gameObject.SetActive(true);
            // set  camer to position
            Vector3 camStartposition = Camera.main.transform.position;
            Camera.main.transform.position = _cameraDestination.position;

            _blackScreen.Play("BS_fadein");
            _audio.PlayOneShot(_wolfSounds[0], 0.1f);
            yield return new WaitForSeconds(1f);
            _audio.PlayOneShot(_wolfSounds[1], 0.5f);
            yield return new WaitForSeconds(_camStayTime);
            _blackScreen.Play("BS_fadeout");
            yield return new WaitForSeconds(_blackScreenTime);
            Camera.main.transform.position = camStartposition;
            _blackScreen.Play("BS_fadein");
            _spawner.SetActive(false);
            _camLight.gameObject.SetActive(false);
            camController.enabled = true;
            Final();
        }               

        
    }

}
