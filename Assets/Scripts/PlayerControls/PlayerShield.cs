using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : playerControls
{
    bool shielding;
    private Animator animator;
    private PlayerMovement playerMovement;
    protected override void Awake()
    {
        base.Awake();
        controls.Player.Shielding.performed += input => setShielding(input.ReadValueAsButton()); // += adds a function delegate to the list of delegates called when, in this case, the interact button is pressed. The input => Interact() stuff is a lambda expression.
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setShielding(bool input)
    {

        animator.SetBool("shielding", input);

        if (input)
            playerMovement.speed = 4;
        else
            playerMovement.speed = 8;


    }
}
