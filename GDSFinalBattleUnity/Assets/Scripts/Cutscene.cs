using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    protected bool _play = false;

    [SerializeField] private float _unmovableTime = 1f;

    protected Animator _letterbox;

    private bool _skip = false;
    private float _holdTime = 1f;
    private float _currentHoldTime = 0f;
    protected GameObject _anyKey;
    protected Slider _slider;

    protected PlayerController _playerController;
    protected NavMeshAgent _playerAgent;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _letterbox = GameObject.FindGameObjectWithTag("LetterBox").GetComponent<Animator>();

        _anyKey = GameObject.FindGameObjectWithTag("AnyKey");
        _slider = _anyKey.GetComponentInChildren<Slider>();
        _slider.value = _currentHoldTime;

        _playerAgent = PlayerManager.instance.player.GetComponent<NavMeshAgent>();
        _playerController = PlayerManager.instance.player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.anyKey && _skip == false)
        {
            _currentHoldTime += Time.deltaTime;
            if (_currentHoldTime >= _holdTime)
            {
                _skip = true;
                Final();
            }
        }
        else
        {
            _currentHoldTime = 0f;
        }

        _slider.value = _currentHoldTime;
    }

    protected virtual void StartEvent()
    {
        _anyKey.SetActive(true);
        _playerController.enabled = false;
        _letterbox.SetTrigger("LetterIn");
    }

    protected virtual void Final()
    {
        _skip = true;
        _letterbox.SetTrigger("LetterOut");
        _slider.value = 0f;
        _anyKey.SetActive(false);
        StartCoroutine(DestroyObject());
    }

    protected virtual IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(_unmovableTime);
        _playerController.enabled = true;
        Destroy(gameObject);
    }
}
