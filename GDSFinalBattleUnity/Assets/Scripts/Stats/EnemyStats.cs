﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField] private GameObject _ragdollPrefab;

    public override void Die()
    {
        base.Die();
        // add ragdoll effect / death animation
        GameObject prefab = Instantiate(_ragdollPrefab, transform.position, transform.rotation);
        prefab.transform.localScale = transform.localScale;
        Destroy(gameObject);
    }

    
}
