using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneTrigger : MonoBehaviour
{
    private Animator _lightAnim;
    private GameObject _finalSceneTrigger;
    // Start is called before the first frame update
    void Start()
    {
        _lightAnim = GameObject.FindGameObjectWithTag("BabciaLight").GetComponent< Animator >();
        _finalSceneTrigger = GameObject.FindGameObjectWithTag("FinalSceneTrigger");
        GetComponent<CharacterStats>().OnCharacterDie += OnDie;
    }

    // Update is called once per frame
    private void OnDie()
    {
        _lightAnim.SetTrigger("LightOn");
        // BabciaCutscene wyłącza trigger
        _finalSceneTrigger.transform.GetChild(0).gameObject.SetActive(true);
    }
}
