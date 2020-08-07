using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableCorpse : Interactable
{
    PlayerManager playerManager;
    CharacterStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.instance;
        playerStats = playerManager.player.GetComponent<CharacterStats>();
        
    }

    public override void Interact()
    {
        base.Interact();

        Eat();

    }

    private void Eat()
    {
        Debug.Log("EATING! HUNGRY MOTHERFUCKER");
        playerStats.Heal();
    }
}
