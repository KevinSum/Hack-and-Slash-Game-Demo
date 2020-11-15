using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : playerControls
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private Vector2 movementInput;
    private Rigidbody2D rigidbody;
    private float facingAngle;

    [SerializeField] private GameObject upHitbox;
    [SerializeField] private GameObject leftHitbox;
    [SerializeField] private GameObject rightHitbox;
    [SerializeField] private GameObject downHitbox;


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
            animator.SetFloat("moveX", movementInput.x);
            animator.SetFloat("moveY", movementInput.y);
            StartCoroutine(enableHitbox());

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

    // Enable attack hitboxes according to facing direction
    IEnumerator enableHitbox()
    {
        facingAngle = playerMovement.getFacingAngle();

        if (-Mathf.PI / 4 < facingAngle && facingAngle < Mathf.PI / 4) // facing up
        {
            upHitbox.SetActive(true);
            Debug.Log("Up attack");
            yield return 0;// Wait 1 frame
            upHitbox.SetActive(false);
        }
        if (-Mathf.PI * 3 / 4 < facingAngle && facingAngle < -Mathf.PI / 4) // facing left
        {
            leftHitbox.SetActive(true);
            Debug.Log("Left attack");
            yield return 0;// Wait 1 frame
            leftHitbox.SetActive(false);
        }
        if (Mathf.PI / 4 < facingAngle && facingAngle < Mathf.PI * 3 / 4) // facing right
        {
            rightHitbox.SetActive(true);
            Debug.Log("Right attack");
            yield return 0;// Wait 1 frame
            rightHitbox.SetActive(false);
        }
        if (facingAngle < -Mathf.PI * 3 / 4 ||  Mathf.PI * 3 / 4 < facingAngle) // facing down
        {
            downHitbox.SetActive(true);
            Debug.Log("Down attack");
            yield return 0;// Wait 1 frame
            downHitbox.SetActive(false);
        }

    }
}
