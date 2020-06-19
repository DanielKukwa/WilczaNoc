using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    AnimationClip[] currentAttackAnimSet;

    const float locomotionAnimationSmoothTime = 0.1f;
    NavMeshAgent agent;
    Animator animator;
    CharacterCombat combat;
    public AnimatorOverrideController overrideController;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        if(overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        
        animator.runtimeAnimatorController = overrideController;
        //overrideController["rig|K_FastSlash1"]

        currentAttackAnimSet = defaultAttackAnimSet;

        combat.OnAttack += OnAttack;
        combat.OnAttack2 += OnAttack2;

    }

    // Update is called once per frame
    void Update()
    {

        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
        animator.SetBool("inCombat", combat.InCombat);
    }

    void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];

    }

    void OnAttack2()
    {
        animator.SetTrigger("attack2");
        int attack2Index = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attack2Index];

    }

    public void OnJump()
    {
        animator.SetTrigger("Jump");
    }

    public void OnMark()
    {
        animator.SetTrigger("Mark");
    }
}
