﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

// System to print dialogue into a dialogue UI when player interacts with something
public class Dialogue : playerControls
{
    [Header("Yarn Spinner Stuff")]
    [SerializeField] protected DialogueRunner dialogueRunner;
    [SerializeField] protected DialogueUI dialogueUI;
    [SerializeField] private string startNode = ""; // Name of starting node
    [SerializeField] private YarnProgram yarnScriptToLoad; // Yarn script to be used (This is optional, as a pre-loaded script could also be used)
    private bool inDialogue;

    protected GameObject player;
    protected Animator playerAnimator; // For accessing parameters


    protected override void Awake()
    {
        base.Awake();
        controls.Player.Interact.performed += input => Interact(); // += adds a function delegate to the list of delegates called when, in this case, the interact button is pressed. The input => Interact() stuff is a lambda expression.
    }

    protected virtual void Start()
    {
        dialogueRunner.Clear(); // Clear all loaded nodes upon start. This is needed, as the dialogueRunner will try to load the same nodes when entering a scene multiple times.
        if (yarnScriptToLoad != null)
            dialogueRunner.Add(yarnScriptToLoad);

        player = GameObject.Find("Player");
        playerAnimator = player.GetComponent<Animator>();
    }

    protected virtual void Interact()
    {
        // Check if touching an object with dialogue attached or already in dialogue (latter is important for cutscenes, since player isn't touching anyone)
        if (this.GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>()) || inDialogue) 
        {
            StartDialogue();
        }
    }

    private void Update()
    {
        // If we've reached the end of the dialogue, set moveable.
        // Note: Local inDialogue bool is to make sure we don't accidently set of the other dialogue boxes (hence why it's checked here). The animator bool is to be used for other classes (e.g. can player still turn).
        if (!dialogueRunner.IsDialogueRunning && inDialogue)
        {
            inDialogue = false;
            playerAnimator.SetBool("canMove", true);
            playerAnimator.SetBool("inDialogue", false);
        }
    }

    public virtual void StartDialogue()
    {
        if (!dialogueRunner.IsDialogueRunning)
        {
            inDialogue = true;
            playerAnimator.SetBool("canMove", false);
            playerAnimator.SetBool("inDialogue", true);
            dialogueRunner.StartDialogue(startNode);
        }
        else
        {
            dialogueUI.MarkLineComplete();   
        }
    }


}
