using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CutsceneStart : MonoBehaviour
{
    private float _holdTime = 1f;
    private float _currentHoldTime = 0f;
    [SerializeField] private float _blackScreenTime;
    [SerializeField] private Animator _blackScreen;
    [SerializeField] private Animator _letterbox;
    [SerializeField] private GameObject _anyKey;
    private Slider _slider;
    [SerializeField] private Transform _destination;
    [SerializeField] private float _destinationDistanceOffset;
    [SerializeField] private float _destroyTime = 1f;
    private PlayerController _playerController;
    private PlayerMotor _playerMotor;
    private NavMeshAgent _playerAgent;
    [SerializeField] private GameObject _doorSmash;

    // Start is called before the first frame update
    void Start()
    {
        _playerAgent = PlayerManager.instance.player.GetComponent<NavMeshAgent>();
        _playerController = PlayerManager.instance.player.GetComponent<PlayerController>();
        _playerController.enabled = false;
        _anyKey.SetActive(true);
        _slider = _anyKey.GetComponentInChildren<Slider>();
        _slider.value = _currentHoldTime;
        _letterbox.SetTrigger("LetterIn");
        StartCoroutine(FadeIn());       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            _currentHoldTime += Time.deltaTime;
            if(_currentHoldTime >= _holdTime)
            {
                Final();
            }
        }
        else
        {
            _currentHoldTime = 0f;
        }

        _slider.value = _currentHoldTime;

    }

    IEnumerator JourneyStarts()
    {
        _playerAgent.SetDestination(_destination.position);
        while (Vector3.Distance(_playerAgent.transform.position, _destination.position ) > _destinationDistanceOffset)
        {
            Debug.Log("Move");
            yield return null;
        }
        Final();
    }

    private void Final()
    {
        PlayerManager.instance.player.transform.position = _destination.position;
        PlayerManager.instance.player.transform.rotation = _destination.rotation;
        _letterbox.SetTrigger("LetterOut");
        _blackScreen.Play("BS_clear");
        _playerController.enabled = true;
        _slider.value = 0f;
        _anyKey.SetActive(false);
        Destroy(gameObject, _destroyTime);
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(_blackScreenTime);
        _doorSmash.SetActive(true);
        _blackScreen.SetTrigger("FadeIn");
        StartCoroutine(JourneyStarts());
    }
}
