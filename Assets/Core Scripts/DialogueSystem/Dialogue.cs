using System.Collections;
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

    protected GameObject player;

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
    }

    protected virtual void Interact()
    {
        if (this.GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>()))
        {
            StartDialogue();
        }
    }

    protected virtual void StartDialogue()
    {
        if (!dialogueRunner.IsDialogueRunning)
            dialogueRunner.StartDialogue(startNode);
        else
            dialogueUI.MarkLineComplete();
    }
}
