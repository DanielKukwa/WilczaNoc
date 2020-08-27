using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentDestoyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        WolfNormalController wolfNormalController = other.gameObject.GetComponent<WolfNormalController>();
        if (wolfNormalController)
        {
            NavMeshAgent agent = other.gameObject.GetComponent<NavMeshAgent>();
            Destroy(agent);
            WolfAnimator wolfAnimator = other.gameObject.GetComponent<WolfAnimator>();
            wolfAnimator.Animator.SetFloat("speedPercent", 0);
            Destroy(wolfAnimator);
            Destroy(wolfNormalController);
        }
    }
}
