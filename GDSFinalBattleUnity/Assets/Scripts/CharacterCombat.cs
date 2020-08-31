using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats myStats;
    GameObject handAxe;
    NavMeshAgent agent;

    const float combatCooldown = 5f;
    float lastAttackTime;
    public bool axeActive = false;
    
    [Header("First Attack")]
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = 0.6f;


    [Header("Second Attack")]
    public float attack2Speed = 1f;
    private float attack2Cooldown = 0f;
    public float attack2Delay = 0.6f;
    public float animDelay = 2f;


    public bool InCombat{ get; private set; }

    public event System.Action OnAttack;
    public event System.Action OnAttack2;
    public event System.Action OnDeleyedAttack;
    public event System.Action OnDeleyedAttack2;

    private GameObject bloodSplash;
    private GameObject bloodDecay;
    private RaycastHit hitInfo;

    

    protected virtual void Start()
    {
        myStats = GetComponent<CharacterStats>();
        var blood = Resources.Load("Prefabs/Blood/HitSplashNormal");
        bloodDecay = (GameObject) Resources.Load("Prefabs/Blood/BloodDecay");
        //var blood = Resources.Load("Prefabs/Blood/BloodSplashV3");
        bloodSplash = blood as GameObject;

        handAxe = GameObject.Find("HandAxe");
        agent = GetComponent<NavMeshAgent>();
    }

     void Update()
    {
        attackCooldown -= Time.deltaTime;
        attack2Cooldown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }

        if(axeActive == true)
        {
            handAxe.SetActive(true);
        }
     

    }

    public void Attack(CharacterStats targetStats)
    {
        if (this.gameObject.tag == "Player" && handAxe.active)
        {
            if (attackCooldown <= 0f && targetStats != null)
            {
                StartCoroutine(DoDamage(targetStats, attackDelay));

                if (OnAttack != null)
                    OnAttack();

                attackCooldown = 1f / attackSpeed;
                InCombat = true;
                lastAttackTime = Time.time;
            }
        } else if(this.gameObject.tag != "Player")
        {
            if (attackCooldown <= 0f && targetStats != null)
            {
                StartCoroutine(DoDamage(targetStats, attackDelay));

                if (OnAttack != null)
                    OnAttack();

                attackCooldown = 1f / attackSpeed;
                InCombat = true;
                lastAttackTime = Time.time;
            }
        }
    }

    public void Attack2(CharacterStats targetStats)
    {
        if (attack2Cooldown <= 0f && targetStats != null && handAxe.active)
        {
            agent.enabled = false;
            StartCoroutine(DoDamage2(targetStats, attack2Delay));

            if (OnAttack2 != null)
                OnAttack2();

            attack2Cooldown = 1f / attack2Speed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        if (stats.gameObject.tag == "Player") AudioManager.Instance.PlayWolfAttack();

        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.GetValue());

        OnDeleyedAttack?.Invoke();
        OnDeleyedAttack = null;

        if(stats.gameObject.tag != "Thing")
        {
            Instantiate(bloodSplash, new Vector3(stats.transform.position.x, 1.3f, stats.transform.position.z), Quaternion.LookRotation(hitInfo.normal));
            if (stats.gameObject.tag != "Player")
            {
                GameObject bloodD = Instantiate(bloodDecay, new Vector3(stats.transform.position.x, 0.015f, stats.transform.position.z), Quaternion.LookRotation(Vector3.up));
                float randZ = Random.Range(0, 359);
                bloodD.transform.Rotate(0, 0, randZ);
            }
        }      
      
        if (stats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }

    IEnumerator DoDamage2(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.GetValue2());

        OnDeleyedAttack2?.Invoke();
        OnDeleyedAttack2 = null;

        Instantiate(bloodSplash, new Vector3(stats.transform.position.x, 1.3f, stats.transform.position.z), Quaternion.LookRotation(hitInfo.normal));

        {
            GameObject bloodD = Instantiate(bloodDecay, new Vector3(stats.transform.position.x, 0.015f, stats.transform.position.z), Quaternion.LookRotation(Vector3.up));
            float randZ = Random.Range(0, 359);
            bloodD.transform.Rotate(0, 0, randZ);
        }
        if (stats.currentHealth <= 0)
        {
            InCombat = false;
        }
        yield return new WaitForSeconds(animDelay);
        agent.enabled = true;
        
    }

}
