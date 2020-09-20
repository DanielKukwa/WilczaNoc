using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneTrigger : MonoBehaviour
{
    private Animator _lightAnim;
    private GameObject _finalSceneTrigger;

    private OutlineController outController;
    private OutlineVisibility outVis;
    // Start is called before the first frame update
    void Start()
    {
        _lightAnim = GameObject.FindGameObjectWithTag("BabciaLight").GetComponent< Animator >();
        _finalSceneTrigger = GameObject.FindGameObjectWithTag("FinalSceneTrigger");
        outController = _finalSceneTrigger.GetComponentInChildren<OutlineController>();
        outVis = _finalSceneTrigger.GetComponentInChildren<OutlineVisibility>();
        //SelectecedInfo selectecedInfo = _finalSceneTrigger.GetComponentInChildren<SelectecedInfo>();
       // outController.SetSelectedInfo(selectecedInfo);
        outController.enabled = false;
        outVis.enabled = false;
        
        //_finalSceneTrigger.SetActive(false);
        GetComponent<CharacterStats>().OnCharacterDie += OnDie;
    }

    // Update is called once per frame
    private void OnDie()
    {
        _lightAnim.SetTrigger("LightOn");
        outController.enabled = true;
        outVis.enabled = true;
        _finalSceneTrigger.SetActive(true);
    }
}
