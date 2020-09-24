
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class CharacterStats : MonoBehaviour
{
    public event Action OnCharacterDie;
    // Health
    public int maxHealth = 100;
    public int currentHealth; //{ get; private set; }
    private Healthbar _healthbar;
    private bool _firstHit = false;
    public float healAnimTime = 1.5f;
    bool gameEnded = false;
    public float restartDelay = 1f;
    Animator animator;
    public bool godMode = false;
    NavMeshAgent agent;
    SceneLoader sceneLoader;
    CharacterCombat combat;
    PlayerMotor motor;
    

    [Header("Damage")]
    public int damageValue;
    public int damageValue2;
    public int damageModifier;
    // Set current health to max health
    // when starting the game.
    void Awake()
    {
        currentHealth = maxHealth;
        damageModifier = 1;
    }

    protected virtual void Start()
    {
        _healthbar = GetComponentInChildren<Healthbar>();
        if (_healthbar)
        {
            //if(gameObject.tag == "Player")
            //{
            //    _healthbar.Animator.SetTrigger("Show");
            //}

            _healthbar.SetSliderMaxValue(maxHealth);
        }
        

        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        combat = GetComponent<CharacterCombat>();
        motor = GetComponent<PlayerMotor>();


    }


    public int GetValue()
    {
        return damageValue * damageModifier; 
    }

    public int GetValue2()
    {
        return damageValue2 * damageModifier;
    }

    // Damage the character
    public void TakeDamage(int damage)
    {
        // Subtract the armor value

        // Damage the character
        currentHealth -= damage;
        //Debug.Log(transform.name + " takes " + damage + " damage.");
        if (_healthbar)
        {
            if(/*gameObject.tag != "Player" &&*/ _firstHit == false)
            {
                _firstHit = true;
                _healthbar.Animator.SetTrigger("Show");
            }
            _healthbar.UpdateHealth(currentHealth);
        }
        
        if(animator) animator.SetTrigger("hurt");
        //IncreaseDamage();    UNCOMMENT IF NEEDED

        if (this.tag != "Player")
        {
            CameraShake.Instance.ShakeCamera();
            AudioManager.Instance.PlayImpactSounds();
            if (this.tag != "Thing")
            {
                AudioManager.Instance.PlayWolfHit();
            }
        }
        // If health reaches zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage2(int damage2)
    {
        // Subtract the armor value


        // Damage the character
        currentHealth -= damage2;
        Debug.Log(transform.name + " takes " + damage2 + " damage.");
        if (_healthbar)
        {
            if (/*gameObject.tag != "Player" && */_firstHit == false)
            {
                _firstHit = true;
                _healthbar.Animator.SetTrigger("Show");
            }
            _healthbar.UpdateHealth(currentHealth);
        }
        //IncreaseDamage();  UNCOMMENT IF NEEDED

        if (this.tag != "Player")
        {
            CameraShake.Instance.ShakeCamera();          
            AudioManager.Instance.PlayImpactSounds();
            if (this.tag != "Thing")
            {
                AudioManager.Instance.PlayWolfHit();
            }
        }
        // If health reaches zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal()
    {
        agent.enabled = false;
        StartCoroutine(DoHeal(healAnimTime));
        Debug.Log("HEALED!");
        _healthbar.UpdateHealth(currentHealth);


    }

    public IEnumerator DoHeal(float animTime)
    {
        // UNCOMMENT IF HALF HEAL NEEDED
        //if (currentHealth <= maxHealth / 2)
        //{
        //    currentHealth += (maxHealth / 2);
        //}
        //else
        //{
        //}

        currentHealth += (maxHealth - currentHealth);

        animator.SetTrigger("eat");

        yield return new WaitForSeconds(animTime);
        agent.enabled = true;
    }


    public virtual void Die()
    {
        OnCharacterDie?.Invoke();
        // This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
        if(this.tag == "Player" && godMode == false)
        {
            if (gameEnded == false)
            {
                animator = GetComponentInChildren<Animator>();
                animator.SetTrigger("died");
                gameEnded = true;
                agent.isStopped = true;
                combat.DisableAttacks();
                motor.enabled = false;
                Invoke("Restart", restartDelay);
            }
        }
    }


    void Restart()
    {

        sceneLoader.LoadGameOver();
    }


    public void IncreaseDamage()
    {

        //if (this.tag == "Player")
        //{
            if (currentHealth <= 0.75 * maxHealth && currentHealth > 0.5 * maxHealth)
            {
                Debug.Log("DAMAGE1");
                damageModifier = 1 + 1/2;
                //damageValue = damageValue * damageModifier;
                
            }
            else if (currentHealth <= 0.5 * maxHealth && currentHealth > 0.25 * maxHealth)
            {
                Debug.Log("DAMAGE2");
                damageModifier = 2;
                //damageValue = damageValue * damageModifier;
            }
            else if (currentHealth < 0.25 * maxHealth)
            {
                Debug.Log("DAMAGE3");
                damageModifier = 4;
                //damageValue = damageValue * damageModifier;
            }
            else
            {
                damageModifier = 1;
            }


        //}

    }
}
