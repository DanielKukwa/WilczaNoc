using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentDestoyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
        {
            WolfNormalController wolfNormalController = other.gameObject.GetComponent<WolfNormalController>();
            if (wolfNormalController)
            {
                NavMeshAgent agent = other.gameObject.GetComponent<NavMeshAgent>();
                Destroy(agent);
                WolfAnimator wolfAnimator = other.gameObject.GetComponent<WolfAnimator>();
                wolfAnimator.Animator.SetFloat("speedPercent", 0);
                wolfNormalController.gameObject.AddComponent<NavMeshObstacle>();
                Destroy(wolfAnimator);
                Destroy(wolfNormalController);
            }
        }
        
    }
}
