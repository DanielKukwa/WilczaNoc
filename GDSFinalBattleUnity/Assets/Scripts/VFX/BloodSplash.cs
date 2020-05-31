using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplash : MonoBehaviour
{
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;

    [SerializeField] private GameObject _bloodPaddlePrefab;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("particle hits");
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Collider collider = other.GetComponent<Collider>();
        if (collider != null) Debug.Log("smtg");

        int i = 0;
        while (i < numCollisionEvents)
        {
            if (collider)
            {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 dir = collisionEvents[i].normal;
                float offset = Random.RandomRange(0.01f, 0.1f);
                GameObject bp = Instantiate(_bloodPaddlePrefab, pos ,Quaternion.LookRotation(-dir));
            }                    
            i++;
        }
    }
}
