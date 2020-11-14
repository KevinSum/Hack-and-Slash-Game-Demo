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
    private Vector2 movementInput;
    private Rigidbody2D rigidbody;

    private bool attackQueued;

    protected override void Awake()
    {
        base.Awake();
        controls.Player.Attack.performed += input => AttackInput();
        controls.Player.Move.performed += input => movementInput = input.ReadValue<Vector2>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        rigidbody = GetComponent<Rigidbody2D>();
        
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
        // make sure we're not in dialogue or cutscene
        if (!animator.GetBool("inDialogue"))
        {
            // This is only to queue state transitions in the animator. The behaviour for what happens when states 
            // are enetered/exited are found in Attacking1 and Attacking 2 scripts, which are bahaviours in the states 
            // of the same name in the animator
            animator.SetFloat("moveX", movementInput.x);
            animator.SetFloat("moveY", movementInput.y);
            playerMovement.spriteFlipCheck();

            Vector2 direction = new Vector2(movementInput.x, movementInput.y);
            rigidbody.AddForce(direction, ForceMode2D.Impulse);

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

    // Enable attack hitboxes according to facing direction
    IEnumerator enableHitbox()
    {

    }
}
