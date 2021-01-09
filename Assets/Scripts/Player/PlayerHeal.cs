using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeal : PlayerControls
{
    Animator animator;
    protected override void Awake()
    {
        base.Awake();
        controls.Player.Heal.performed += input => Heal(); // += adds a function delegate to the list of delegates called when, in this case, the interact button is pressed. The input => Interact() stuff is a lambda expression.
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ----- OLD DIALOGUE SYSTEM -------------
        if (Input.GetButtonDown("heal"))
            Heal();
        //------------------------
    }
    void Heal()
    {
        animator.SetBool("healing", true);
    }
}
