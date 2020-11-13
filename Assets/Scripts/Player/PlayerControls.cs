using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Initialize and enable Input Actions for player controls
public class playerControls : MonoBehaviour
{
    protected Controls controls;
    protected virtual void Awake()
    {
        controls = new Controls();
    }

    // Update is called once per frame
    protected virtual void OnEnable()
    {
        controls.Player.Enable();
    }

    protected virtual void OnDisable()
    {
        controls.Player.Disable();
    }
}
