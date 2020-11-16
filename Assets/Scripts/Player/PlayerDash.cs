using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : PlayerControls
{
    PlayerMovement playerMovement;
    Rigidbody2D rigidbody2D;
    [SerializeField] float dashThrust;
    protected override void Awake()
    {
        base.Awake();
        controls.Player.Dash.performed += input => Dash(); // += adds a function delegate to the list of delegates called when, in this case, the interact button is pressed. The input => Interact() stuff is a lambda expression.
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Dash()
    {
        StartCoroutine(DashCoroutine(rigidbody2D));
    }

    private IEnumerator DashCoroutine(Rigidbody2D rigidbody2D)
    {
        Vector2 force = playerMovement.getFacingAngleVec().normalized * dashThrust;

        rigidbody2D.velocity = force;
        yield return new WaitForSeconds(.3f);

        rigidbody2D.velocity = new Vector2();
    }
}
