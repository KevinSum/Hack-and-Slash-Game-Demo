using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : PlayerControls
{
    Animator playerAnimator;
    PlayerMovement playerMovement;
    Rigidbody2D rigidbody2D;
    [SerializeField] float dashThrust;
    [SerializeField] float invicibleTime; // Should probably be set to roll animation length
    protected override void Awake()
    {
        base.Awake();
        controls.Player.Dash.performed += input => Dash(); // += adds a function delegate to the list of delegates called when, in this case, the interact button is pressed. The input => Interact() stuff is a lambda expression.
    }

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ----- OLD DIALOGUE SYSTEM -------------
        if (Input.GetButtonDown("dash"))
            Dash();
        //------------------------
    }

    void Dash()
    {
        StartCoroutine(DashCoroutine(rigidbody2D));
    }

    private IEnumerator DashCoroutine(Rigidbody2D rigidbody2D)
    {
        playerAnimator.SetTrigger("dash");
        playerAnimator.SetBool("canMove", false);
        Vector2 force = playerMovement.getFacingAngleVec().normalized * dashThrust;

        rigidbody2D.velocity = force;
        yield return new WaitForSeconds(invicibleTime);

        rigidbody2D.velocity = new Vector2();
        playerAnimator.SetBool("canMove", true);
    }
}
