using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : playerControls
{
    private Animator animator;
    private AnimationClip attackingAnimation;
    float animationLength;
    private bool attacking = false;
    private PlayerMovement playerMovement;

    private bool attackQueued;

    protected override void Awake()
    {
        base.Awake();
        controls.Player.Attack.performed += input => AttackInput();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        animationLength = 0.2f;
}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AttackInput()
    {
        // This is only to queue state transitions in the animator. The behaviour for what happens when states 
        // are enetered/exited are found in Attacking1 and Attacking 2 scripts, which are bahaviours in the states 
        // of the same name in the animator

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking1"))
        {
            animator.SetBool("queueAttack2", true);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking2"))
        {
            animator.SetBool("queueAttack1", true);
        }
        else
        {
            animator.SetBool("startAttack", true);
        }

    }
}
