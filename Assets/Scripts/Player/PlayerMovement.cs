using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

// Player movement, movement animation etc
public class PlayerMovement : PlayerControls
{
    [SerializeField] public float speed;
    private SpriteRenderer spriteRenderer;
    public Vector2 movementInput;
    private Rigidbody2D playerRigidBody;
    private Animator animator;
    [SerializeField] private GameObject sideHitbox;

    [SerializeField] private bool facingRight;
    [SerializeField] private float facingAngle;
    [SerializeField] private Vector2 facingAngleVec;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        NPC_dialogueRunner = GameObject.Find("NPC Dialogue System").GetComponent<DialogueRunner>();
        objectDialogueRunner = GameObject.Find("Object Dialogue System").GetComponent<DialogueRunner>();
    }

    // Do physics engine stuff in FixedUpdate(). Everything else in Update()
    void FixedUpdate()
    {
        //------ OLD MOVEMENT SYSTEM------------
        movementInput = Vector2.zero;
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        // -------------------------------

        if (movementInput.x !=0 || movementInput.y != 0) // make sure to set facing angle if joystick is moving/moved
        {
            facingAngle = Mathf.Atan2(movementInput.x, movementInput.y); // Current facing angle in radians
            facingAngleVec = movementInput;
        }

        // Move player if joystick is moved. Don't move and set no animation if currently in dialogue
        if (movementInput != Vector2.zero && !NPC_dialogueRunner.IsDialogueRunning && !objectDialogueRunner.IsDialogueRunning && animator.GetBool("canMove"))

        {
            // Set animator parameters. The animator will play animation based on these (as well as the 'running' bool parameter)
            setAnimatorDirection(movementInput);

            // If player can move (e.g. not in attack animation)
            if (animator.GetBool("canMove"))
            {
                animator.SetBool("running", true);
                movePlayer();
            }
            
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
    }

    public void spriteFlipCheck() // Flip sprite if needed. Hopefully can remove this in final version
    {
        // If facing angle is between 45 and 135 deg, but facingRight is false, then set it true and flip sprite. And vice-versa for all other angles.
        if (facingAngle > Mathf.PI / 4 && facingAngle < Mathf.PI * 3 / 4)
        {
            if (!facingRight)
            {
                facingRight = true;
                spriteRenderer.flipX = false;
                sideHitbox.transform.localScale = new Vector3(1, 1, 1);
            }
        } else
        {
            if (facingRight)
            {
                facingRight = false;
                spriteRenderer.flipX = true;
                sideHitbox.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

    }

    public float getFacingAngle()
    {
        return facingAngle;
    }

    public Vector2 getFacingAngleVec()
    {
        return facingAngleVec;
    }

    // Function to send integer values (1, -1 or 0) of x and y movement values to animator. This fixes issues with activating the correct attacking hitboxes (is an issue with the blend trees)
    public void setAnimatorDirection(Vector2 movementInput)
    {
        if(Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
        {
            animator.SetFloat("moveX", movementInput.x * (1 / Mathf.Abs(movementInput.x)));
            animator.SetFloat("moveY", 0);
        }
        else
        {
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", movementInput.y * (1 / Mathf.Abs(movementInput.y)));
        }
    }

}
