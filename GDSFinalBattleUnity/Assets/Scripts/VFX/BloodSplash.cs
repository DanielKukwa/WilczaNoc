using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplash : MonoBehaviour
{
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;
    private static int _bloodCounter = 0;

    [SerializeField] private GameObject _bloodPaddlePrefabGround;
    [SerializeField] private GameObject _bloodPaddlePrefabOther;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Collider collider = other.GetComponent<Collider>();

        int i = 0;
        while (i < numCollisionEvents)
        {
            if (collider)
            {
                Vector3 hitPoint = collisionEvents[i].intersection;
                Vector3 planeNormal = collisionEvents[i].normal;

                Vector3 dirOnPlane = Vector3.ProjectOnPlane(collisionEvents[i].velocity, planeNormal);
                // normalize direction on plane vector
                dirOnPlane = dirOnPlane.normalized;

                Debug.Log(planeNormal.x + " | " + planeNormal.y + " | " + planeNormal.z);
                if (planeNormal.y >= 0.99f)
                {
                    GameObject bloodPaddle = Instantiate(_bloodPaddlePrefabGround, hitPoint, Quaternion.LookRotation(dirOnPlane));
                }
                else
                {
                    GameObject bloodPaddle = Instantiate(_bloodPaddlePrefabOther, hitPoint, Quaternion.LookRotation(planeNormal));
                    // get blood sprte vector up
                    Vector3 bloodUp = bloodPaddle.transform.up;

                    // angle between bloodUp and direction on plane
                    float angle = Mathf.Acos(Vector3.Dot(bloodUp, dirOnPlane) / Vector3.Distance(Vector3.zero, bloodUp) * Vector3.Distance(Vector3.zero, dirOnPlane));

                    // change to degress
                    angle = angle * 180 / Mathf.PI;

                    // check if direction dir on plane is on front or behind blood up vector
                    if (Vector3.Cross(bloodUp, dirOnPlane).z >= 0)
                    {
                        angle = 360 - angle;
                    }

                    Debug.Log(angle);

                    bloodPaddle.transform.Rotate(0, 0, angle);

                }
            }                    
            i++;
        }
    }

}
