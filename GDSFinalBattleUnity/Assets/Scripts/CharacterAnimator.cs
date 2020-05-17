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
    AnimatorOverrideController overrideController;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;
        //overrideController["rig|K_FastSlash1"]

        currentAttackAnimSet = defaultAttackAnimSet;

        combat.OnAttack += OnAttack;
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
}
