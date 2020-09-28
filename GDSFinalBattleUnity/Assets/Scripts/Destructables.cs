using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Destructables : Interactable
{
    public System.Action OnHit;
    PlayerManager playerManager;
    CharacterStats myStats;
    PlayerController playerController;
    public bool isFirstAttack;
    private GameObject _particlesSmall;
    //private GameObject _particlesBig;
    private bool _playOnce = false;
    AudioSource audio;

    void Start()
    {
        _particlesSmall = (GameObject)Resources.Load("Prefabs/Enviro/DestructablesSmall");
        //_particlesBig = (GameObject)Resources.Load("Prefabs/Enviro/DestructablesBig");

        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
        playerController = playerManager.player.GetComponent<PlayerController>();

        myStats.OnCharacterDie += Destruct;
        audio = GetComponent<AudioSource>();

    }

    public override void Interact()
    {
        base.Interact();

        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();

        isFirstAttack = playerController.GetAttackInfo();

        if (playerCombat != null && isFirstAttack == true)
        {

            playerCombat.Attack(myStats);

            if (!_playOnce)
            {
                _playOnce = true;
                playerCombat.OnDeleyedAttack += SmallParticles;
                OnHit?.Invoke();
            }

        }
        else if (playerCombat != null && isFirstAttack == false)
        {
            if (!_playOnce)
            {
                _playOnce = true;
                playerCombat.OnDeleyedAttack += SmallParticles;
                OnHit?.Invoke();
            }


        }

        if (this.gameObject.name == "Line")
            audio.Play();

    }

    private void SmallParticles()
    {
        if (this.gameObject.name != "Line")
        {
            Vector3 partPosition = new Vector3(transform.position.x, 1f, transform.position.z);
            Instantiate(_particlesSmall, partPosition, Quaternion.LookRotation(Vector3.up));
        }        
        _playOnce = false;
    }

    private void Destruct()
    {

        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (playerController)
        {

            playerController.RemoveFocus();
            playerController.motor.agent.SetDestination(playerController.transform.position);
        }
        
    }
}
