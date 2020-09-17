﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class Forester : Interactable
{
    PlayerController playerController;
    public bool isFirstAttack;
    CharacterStats myStats;
    [HideInInspector] public Healthbar Healthbar;
    private AudioSource _audio;
    [SerializeField] private AudioClip _dieSound;
    [SerializeField] private AudioSource _normalEnding;
    [SerializeField] private GameObject _alternativeEnding;
    [SerializeField] private float _normalEndingDelay = 2f;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        Healthbar = GetComponentInChildren<Healthbar>();
        Healthbar.gameObject.SetActive(false);

        myStats = GetComponent<CharacterStats>();
        myStats.OnCharacterDie += ForesterDie;
        playerController = PlayerManager.instance.player.GetComponent<PlayerController>();
    }
    public override void Interact()
    {
        base.Interact();

        CharacterCombat playerCombat = PlayerManager.instance.player.GetComponent<CharacterCombat>();

        isFirstAttack = playerController.GetAttackInfo();

        if (playerCombat != null && isFirstAttack == true)
        {

            playerCombat.Attack(myStats);

        }
        else if (playerCombat != null && isFirstAttack == false)
        {
            playerCombat.Attack2(myStats);
        }

    }

    private void ForesterDie()
    {
        Destroy(_alternativeEnding.gameObject);
        _normalEnding.PlayDelayed(_normalEndingDelay);
        PlayerTextures.Instance.ChangeHoodTexture();
        Healthbar.gameObject.SetActive(false);
        _audio.clip = _dieSound;
        _audio.loop = false;
        _audio.Play();
        OutlineVisibility outVis = GetComponentInChildren<OutlineVisibility>();
        Destroy(outVis);
        OutlineController outlineController = GetComponent<OutlineController>();
        Destroy(outlineController);
        myStats.OnCharacterDie -= ForesterDie;
        //Destroy(this);
    }
}
