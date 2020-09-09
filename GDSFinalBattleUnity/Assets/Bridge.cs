using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bridge : MonoBehaviour
{
    private Animator _anim;
    private AudioSource _audio;
    [SerializeField] private GameObject[] _lines;
    [SerializeField] private GameObject[] _wolfToDestroy;
    [SerializeField] private GameObject[] _wolfAlive;
    [SerializeField] private Transform _wolfFinalDestination;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_lines[0] && !_lines[1])
        {
            _anim.SetTrigger("Fall");
            _audio.Play();
            foreach(GameObject wolf in _wolfToDestroy)
            {
                Destroy(wolf.gameObject);
            }
            foreach (GameObject wolf in _wolfAlive)
            {
                if (wolf)
                {
                    WolfNormalController wolfNormal = wolf.GetComponent<WolfNormalController>();
                    Destroy(wolfNormal);
                    NavMeshAgent agent = wolf.GetComponent<NavMeshAgent>();
                    agent.SetDestination(_wolfFinalDestination.position);
                }
            }
            Destroy(this);
        }
    }
}
