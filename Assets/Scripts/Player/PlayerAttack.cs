using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : playerControls
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private Vector2 movementInput;
    private Rigidbody2D rigidbody;
    [SerializeField] private float facingAngle;

    [SerializeField] private float attackThrust;


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
            playerMovement.setAnimatorDirection(movementInput);

            Vector2 direction = new Vector2(movementInput.x, movementInput.y);
            rigidbody.AddForce(direction, ForceMode2D.Impulse);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking1"))
            {
                playerMovement.spriteFlipCheck();
                animator.SetBool("queueAttack2", true);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking2"))
            {
                playerMovement.spriteFlipCheck();
                animator.SetBool("queueAttack1", true);
            }
            else
            {
                animator.SetBool("startAttack", true);
            }
        }
    }


    // Knockback and damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                StartCoroutine(KnockCoroutine(enemy));
            }
        }
    }

    // Apply thrust/knockback to enemy
    private IEnumerator KnockCoroutine(Rigidbody2D enemy)
    {
        Vector2 forceDirection = enemy.transform.position - transform.position;
        Vector2 force = forceDirection.normalized * attackThrust;

        enemy.velocity = force;
        yield return new WaitForSeconds(.3f);

        enemy.velocity = new Vector2();
    }
}
