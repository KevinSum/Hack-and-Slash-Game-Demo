using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Yarn.Unity;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public bool facingRight;
    private Vector2 movementInput;
    private Rigidbody2D playerRigidBody;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    public DialogueRunner NPC_dialogueRunner;
    public DialogueRunner objectDialogueRunner;


    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        NPC_dialogueRunner = GameObject.Find("NPC Dialogue System").GetComponent<DialogueRunner>();
        objectDialogueRunner = GameObject.Find("Object Dialogue System").GetComponent<DialogueRunner>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!NPC_dialogueRunner.IsDialogueRunning && !objectDialogueRunner.IsDialogueRunning)
            movementInput = getMovementInput();
        else
            animator.SetBool("running", false);
    }

    // Do physics engine stuff in FixedUpdate(). Everything else in Update()
    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            animator.SetBool("running", true);
            moveCharacter();
        }
        else
        {
            animator.SetBool("running", false);
        }
    }
    Vector2 getMovementInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void moveCharacter()
    {
        // Move Player
        Vector2 currentpos = new Vector2(transform.position.x, transform.position.y);
        playerRigidBody.MovePosition(currentpos + movementInput.normalized * speed * Time.deltaTime);
        spriteFlipCheck();
        // Set animator parameters. The animator will play animation based on these (as well as the 'running' bool parameter)
        animator.SetFloat("moveX", movementInput.x);
        animator.SetFloat("moveY", movementInput.y);
    }

    void spriteFlipCheck() // Flip sprite if needed
    {
        if(movementInput.x > 0 && facingRight == false || movementInput.x < 0 && facingRight == true)
        {
            facingRight = !facingRight;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

}
