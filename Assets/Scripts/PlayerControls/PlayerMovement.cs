using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

// Player movement, movement animation etc
public class PlayerMovement : playerControls
{
    [SerializeField] public float speed;
    private bool canMove;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer slashingSpriteRenderer;
    private Vector2 movementInput;
    private Rigidbody2D playerRigidBody;
    private Animator animator;

    private bool facingLeft;
    private float facingAngle;

    private DialogueRunner NPC_dialogueRunner;
    private DialogueRunner objectDialogueRunner;

    protected override void Awake()
    {
        base.Awake();
        // Set movementInput whenever joystick is moved
        controls.Player.Move.performed += input => movementInput = input.ReadValue<Vector2>();
        controls.Player.Move.canceled += input => movementInput = Vector2.zero;
    }
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        slashingSpriteRenderer = this.transform.Find("Sword Slashes").GetComponent<SpriteRenderer>();
        NPC_dialogueRunner = GameObject.Find("NPC Dialogue System").GetComponent<DialogueRunner>();
        objectDialogueRunner = GameObject.Find("Object Dialogue System").GetComponent<DialogueRunner>();
        canMove = true;
    }

    // Do physics engine stuff in FixedUpdate(). Everything else in Update()
    void FixedUpdate()
    {
        facingAngle = Mathf.Atan2(movementInput.x, movementInput.y); // Current facing angle in radians

        // Move player if joystick is moved. Don't move and set no animation if currently in dialogue
        if (movementInput != Vector2.zero && !NPC_dialogueRunner.IsDialogueRunning && !objectDialogueRunner.IsDialogueRunning && canMove)
        {
            animator.SetBool("running", true);
            movePlayer();
        }
        else
        {
            animator.SetBool("running", false);
        }
    }

    void movePlayer()
    {
        Vector2 currentpos = new Vector2(transform.position.x, transform.position.y);
        playerRigidBody.MovePosition(currentpos + movementInput.normalized * speed * Time.deltaTime);
        spriteFlipCheck();
        // Set animator parameters. The animator will play animation based on these (as well as the 'running' bool parameter)
        animator.SetFloat("moveX", movementInput.x);
        animator.SetFloat("moveY", movementInput.y);
    }

    void spriteFlipCheck() // Flip sprite if needed
    {
        // If facing angle is between 315 and 405 deg, but facingLeft is false, then set it true and flip sprite. And vice-versa for all other angles.
        if (facingAngle < - Mathf.PI / 4 && facingAngle > - Mathf.PI * 3 / 4)
        {
            if (!facingLeft)
            {
                facingLeft = true;
                playerSpriteRenderer.flipX = true;
                slashingSpriteRenderer.flipX = true;
            }
        } 
        else
        {
            if (facingLeft)
            {
                facingLeft = false;
                playerSpriteRenderer.flipX = false;
                slashingSpriteRenderer.flipX = false;
            }
        }

    }

    public void movementEnabled(bool input)
    {
        canMove = input;
    }

    public float getFacingAngle()
    {
        return facingAngle;
    }

}
