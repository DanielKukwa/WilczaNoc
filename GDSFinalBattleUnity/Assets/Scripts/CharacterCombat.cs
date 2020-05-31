using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats myStats;
    public float attackSpeed = 1f;
    public float attackCooldown = 0f;
    public float attackDelay = 0.6f;
    const float combatCooldown = 5f;
    float lastAttackTime;

    public bool InCombat{ get; private set; }

    public event System.Action OnAttack;

    private GameObject bloodSplash;
    private RaycastHit hitInfo;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
        var blood = Resources.Load("Prefabs/Blood/BloodSplashV3");
        bloodSplash = blood as GameObject;
    }

     void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats)
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

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue());
        Instantiate(bloodSplash, new Vector3(stats.transform.position.x, 1.3f, stats.transform.position.z), Quaternion.LookRotation(hitInfo.normal));

        if (stats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }

}
