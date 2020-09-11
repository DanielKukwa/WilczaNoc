using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestructableFence : MonoBehaviour
{
    Action OnFanceDestroyed;
    private CharacterCombat _playerCombat;
    [SerializeField] private Mesh[] _meshes;
    private MeshFilter _meshFilter;
    private int _durability;
    private int _currentHitCounter;
    private Destructables _destructables;

    private Animator _animator;
    
    private void Start()
    {
        _playerCombat = PlayerManager.instance.player.GetComponent<CharacterCombat>();
        _meshFilter = GetComponent<MeshFilter>();
        _currentHitCounter = 0;
        _durability = _meshes.Length;
        _destructables = GetComponent<Destructables>();
        _destructables.OnHit += SetHitToDeley;
        _animator = GetComponent<Animator>();
    }

    private void SetHitToDeley()
    {
        _playerCombat.OnDeleyedAttack += OnFanceHit;
       // _playerCombat.OnDeleyedAttack2 += OnFanceHit;
    }


    private void OnFanceHit()
    {
        _currentHitCounter++;

        if(_currentHitCounter > _meshes.Length)
        {
            _animator.SetTrigger("Open");
            Destroy(_destructables);
            OutlineVisibility outVis = GetComponent<OutlineVisibility>();
            Destroy(outVis);
            Knife.HDRPOutline.Core.OutlineObject outline = GetComponent<Knife.HDRPOutline.Core.OutlineObject>();
            Destroy(outline);
            OnFanceDestroyed?.Invoke();
            Destroy(this);
        }
        else
        {
            _meshFilter.mesh = _meshes[_currentHitCounter - 1];
        }


        //_playerCombat.OnDeleyedAttack -= OnFanceHit;
        //_playerCombat.OnDeleyedAttack2 -= OnFanceHit;
    }
}
