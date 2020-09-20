using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private CharacterStats _treestats;
    private Animator _anim;
    [SerializeField] private ParticleSystem _destroyParticles;
    [SerializeField] private GameObject _particles;
    [SerializeField] private EventTrigger _eventTrigger;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _treestats = GetComponent<CharacterStats>();
        _treestats.OnCharacterDie += OnTreeDie;

        if (_eventTrigger)
        {
            _eventTrigger.OnEventTrigger += OnTriggerActive;
        }
    }

    public void OnTreeDie()
    {
        _destroyParticles.Play();
        Destroy(gameObject);      
    }

    private void OnTriggerActive()
    {
        _anim.SetTrigger("Fall");
    }

    public void ActivateParticles()
    {
        if(_particles)
        _particles.SetActive(true);
    }
}
