using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEndCutscene : MonoBehaviour
{
    [SerializeField] private float _blackScreenTime = 1f;
    [SerializeField] private Animator _blackScreen;
    private EventTrigger _eventTrigger;
    // Start is called before the first frame update
    private void Start()
    {
        _eventTrigger = GetComponentInChildren<EventTrigger>();
        _eventTrigger.OnEventTrigger += OnTriggerActive;    
    }

  
    private void OnTriggerActive()
    {
        _blackScreen.SetTrigger("FadeOut");
        StartCoroutine(FadeOutAndIn());
    }


    IEnumerator FadeOutAndIn()
    {
        yield return new WaitForSeconds(_blackScreenTime);
        _blackScreen.SetTrigger("FadeIn");
    }

}
