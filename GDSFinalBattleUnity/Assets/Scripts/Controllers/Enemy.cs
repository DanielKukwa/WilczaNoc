﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;
    PlayerController playerController;
    public bool isFirstAttack;

    void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
        playerController = playerManager.player.GetComponent<PlayerController>();
       
    }

      public override void Interact()
    {
        base.Interact();
        
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();

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



}
