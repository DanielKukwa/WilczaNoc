using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRadiusTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] _wolfes;

    private void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach (GameObject g in _wolfes)
            {
                WolfNormalController controller1 = g.GetComponent<WolfNormalController>();
                if (!controller1)
                {
                    WolfJumpController controller2 = g.GetComponent<WolfJumpController>();
                    if (!controller2)
                    {
                        WolfHowlController controller3 = g.GetComponent<WolfHowlController>();
                        controller3.lookRadius = 100f;
                    }
                    else
                    {
                        controller2.lookRadius = 100f;
                    }
                }
                else
                {
                    controller1.lookRadius = 100f;
                }
            }

            Destroy(gameObject);

        }
        
    }
}
