using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CutsceneStart : Cutscene
{

    [SerializeField] private float _blackScreenTime;
    [SerializeField] private Animator _blackScreen;


    [SerializeField] private Transform _destination;
    [SerializeField] private float _destinationDistanceOffset;


    [SerializeField] private GameObject _doorSmash;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        _playerController.enabled = false;

        _letterbox.SetTrigger("LetterIn");
        StartCoroutine(FadeIn());       
    }


    IEnumerator JourneyStarts()
    {
        _playerAgent.SetDestination(_destination.position);
        while (Vector3.Distance(_playerAgent.transform.position, _destination.position ) > _destinationDistanceOffset)
        {
            yield return null;
        }
        Final();
    }

    protected override void Final()
    {
        _playerAgent.SetDestination(_destination.position);
        _letterbox.SetTrigger("LetterOut");
        _blackScreen.Play("BS_clear");       
        _slider.value = 0f;
        _anyKey.SetActive(false);
        StartCoroutine(DestroyObject());
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(_blackScreenTime);
        _doorSmash.SetActive(true);
        _blackScreen.SetTrigger("FadeIn");
        StartCoroutine(JourneyStarts());
    }
}
