using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D playerRigidBody;
    private Vector2 movementInput;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        movementInput = getMovementInput();
    }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            MoveCharacter();
        }
    }
    Vector2 getMovementInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void MoveCharacter()
    {
        Vector2 currentpos = new Vector2(transform.position.x, transform.position.y);
        playerRigidBody.MovePosition(currentpos + movementInput.normalized * speed * Time.deltaTime);
    }
}
