using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField] private GameObject _ragdollPrefab;

    GameObject bloodSplash;
    GameObject hitSplash;

   protected override void Start()
   {
        base.Start();
       bloodSplash = (GameObject)Resources.Load("Prefabs/Blood/BloodSplashV3");
       hitSplash = (GameObject)Resources.Load("Prefabs/Blood/HitSplashBig");
   }

    public override void Die()
    {
        
        if (_ragdollPrefab != null)
        {
            base.Die();
            // add ragdoll effect / death animation
            GameObject prefab = Instantiate(_ragdollPrefab, transform.position, transform.rotation);
            prefab.transform.localScale = transform.localScale;
            Instantiate(bloodSplash, new Vector3(transform.position.x, 1.3f, transform.position.z), Quaternion.LookRotation(Vector3.up));
            Instantiate(hitSplash, new Vector3(transform.position.x, 1.3f, transform.position.z), Quaternion.LookRotation(Vector3.up));
            AudioManager.Instance.PlayWolfDie();
            
        }
        Destroy(gameObject);
    }

    
}
