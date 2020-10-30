using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : playerControls
{
    private Animator playerAnimator;
    private Animator slashingAnimator;
    private GameObject swordSlashes;
    private PlayerMovement playerMovement;

    protected override void Awake()
    {
        base.Awake();
        controls.Player.Attack.performed += input => AttackInput();
    }
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<Animator>();
        swordSlashes = GameObject.Find("Sword Slashes");
        slashingAnimator = swordSlashes.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AttackInput()
    {
        float facingAngle = playerMovement.getFacingAngle() * Mathf.Rad2Deg; // Getting current facing angle and convert to degrees.
        swordSlashes.transform.rotation = Quaternion.Euler(0, 0, -facingAngle);

        // This is only to queue state transitions in the animator. The behaviour for what happens when states 
        // are enetered/exited are found in Attacking1 and Attacking 2 scripts, which are bahaviours in the states 
        // of the same name in the animator

        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attacking1"))
        {
            playerAnimator.SetBool("queueAttack2", true);
            slashingAnimator.SetBool("queueAttack2", true);

        }
        else if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attacking2"))
        {
            playerAnimator.SetBool("queueAttack1", true);
            slashingAnimator.SetBool("queueAttack1", true);
        }
        else
        {
            playerAnimator.SetBool("startAttack", true);
            slashingAnimator.SetBool("startAttack", true);
        }




    }
}
