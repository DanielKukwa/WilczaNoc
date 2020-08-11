using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmisionControl : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private float _fullEmmision = 5f;
    [SerializeField] private float _noEmmision = 10f;
    Renderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _player = PlayerManager.instance.player;
        _renderer = GetComponent<Renderer>();
        _renderer.material.SetFloat("_EmissiveExposureWeight", 0);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        if(distance < _fullEmmision) _renderer.material.SetFloat("_EmissiveExposureWeight", 1);
        else if (distance > _noEmmision) _renderer.material.SetFloat("_EmissiveExposureWeight", 0);
       else
       {
           _renderer.material.SetFloat("_EmissiveExposureWeight", Mathf.Lerp(0 , 1, (_noEmmision - distance) / (_noEmmision - _fullEmmision)));
       }
    }
}
