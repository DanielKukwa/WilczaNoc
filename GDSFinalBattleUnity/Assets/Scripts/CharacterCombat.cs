﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats myStats;

    [Header("First Attack")]
    public float attackSpeed = 1f;
    public float attackCooldown = 0f;
    public float attackDelay = 0.6f;
    const float combatCooldown = 5f;
    float lastAttackTime;

    [Header("Second Attack")]
    public float attack2Speed = 1f;
    public float attack2Cooldown = 0f;
    public float attack2Delay = 0.6f;


    public bool InCombat{ get; private set; }

    public event System.Action OnAttack;
    public event System.Action OnAttack2;

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
        attack2Cooldown -= Time.deltaTime;

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

    public void Attack2(CharacterStats targetStats)
    {
        if (attack2Cooldown <= 0f && targetStats != null)
        {
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
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue());
        Instantiate(bloodSplash, new Vector3(stats.transform.position.x, 1.3f, stats.transform.position.z), Quaternion.LookRotation(hitInfo.normal));

        if (stats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }

    IEnumerator DoDamage2(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage2.GetValue());
        Instantiate(bloodSplash, new Vector3(stats.transform.position.x, 1.3f, stats.transform.position.z), Quaternion.LookRotation(hitInfo.normal));

        if (stats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }

}
