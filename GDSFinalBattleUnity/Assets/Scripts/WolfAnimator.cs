using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfAnimator : MonoBehaviour
{
    //public AnimationClip replaceableAttackAnim;
    //public AnimationClip[] defaultAttackAnimSet;
    //AnimationClip[] currentAttackAnimSet;

    const float locomotionAnimationSmoothTime = 0.1f;
    NavMeshAgent agent;
    [HideInInspector] public Animator Animator;

    CharacterCombat combat;
    //public AnimatorOverrideController overrideController;

    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        // if (overrideController == null)
        // {
        //     overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        // }

        //animator.runtimeAnimatorController = overrideController;
        //overrideController["rig|K_FastSlash1"]

        //currentAttackAnimSet = defaultAttackAnimSet;

        combat.OnAttack += OnAttack;
        //combat.OnAttack2 += OnAttack2;

    }

    // Update is called once per frame
    void Update()
    {

        float speedPercent = agent.velocity.magnitude / agent.speed;
        Animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
        Animator.SetBool("InCombat", combat.InCombat);
    }

    public void SetHowlAnimationLenght(float time)
    {
        StartCoroutine(WaitForAnimator(time));  
    }

    IEnumerator WaitForAnimator(float time)
    {
        while (!Animator)
        {
            yield return null;
        }
        Animator.SetFloat("HowlSpeed", time);
    }

    void OnAttack()
    {
        Animator.SetTrigger("Attack");
        //int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        //overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];

    }

    public void OnHowl()
    {
        Animator.SetTrigger("Howl");
    }

    public void OnMark()
    {
        Animator.SetTrigger("Mark");
    }

    public void OnJump()
    {
        Animator.SetTrigger("Jump");
    }
}
