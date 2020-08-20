using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Forester : Interactable
{
    PlayerController playerController;
    public bool isFirstAttack;
    CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
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
}
