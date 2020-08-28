using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEndCutscene : MonoBehaviour
{
    [SerializeField] private float _blackScreenTime = 1f;
    [SerializeField] private Animator _blackScreen;
    private GameObject _snowOld;
    private GameObject _snowNew;
    private EventTrigger _eventTrigger;
    // Start is called before the first frame update
    private void Start()
    {
        _eventTrigger = GetComponentInChildren<EventTrigger>();
        _eventTrigger.OnEventTrigger += OnTriggerActive;
        _snowOld = GameObject.FindGameObjectWithTag("SnowOld");
        _snowNew = GameObject.FindGameObjectWithTag("SnowNew");
        _snowNew.SetActive(false);
    }

  
    private void OnTriggerActive()
    {
        _blackScreen.SetTrigger("FadeOut");
        StartCoroutine(FadeOutAndIn());
    }


    IEnumerator FadeOutAndIn()
    {
        yield return new WaitForSeconds(_blackScreenTime);
        _snowNew.SetActive(true);
        _snowOld.SetActive(false);
        _blackScreen.SetTrigger("FadeIn");
    }

}
