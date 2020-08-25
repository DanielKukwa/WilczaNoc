using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutscene : Cutscene
{
    [SerializeField] GameObject[] _barricades;
    [SerializeField] private GameObject _shotgun;
    [SerializeField] private GameObject _endingGameObject;
    private Animator _anim;
    [SerializeField] private AudioSource _shotSFX;

    protected override void Start()
    {
        _anim = GetComponent<Animator>();
        _shotgun.gameObject.SetActive(false);
        base.Start();
    }

    protected override void StartEvent()
    {
        base.StartEvent();
        _anim.SetTrigger("Shot");
        //_shotgun.gameObject.SetActive(true);
    }
    // Update is called once per frame
    protected override void Update()
    {
        if (!_play)
        {
            if (!_barricades[0] && !_barricades[1])
            {
                StartEvent();
            }
        }
        else
        {
            base.Update();
        }
    }

    protected override void Final()
    {
        // GO wyłączony w skrpcie LeaveTrigger
        _endingGameObject.SetActive(true);
       // _shotgun.gameObject.SetActive(true);
        Destroy(this.gameObject);
    }

    public void Shot()
    {
        _shotSFX.Play();
        _playerController.GetComponentInChildren<Animator>().SetTrigger("died");
    }
}
