using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class Dialogue : MonoBehaviour
{
    [Header("Yarn Spinner Stuff")]
    [SerializeField] protected DialogueRunner dialogueRunner;
    [SerializeField] protected DialogueUI dialogueUI;
    [SerializeField] private string startNode = ""; // Name of starting node
    [SerializeField] private YarnProgram yarnScriptToLoad; // Yarn script to be used (This is optional, as a pre-loaded script could also be used

    protected GameObject player;
    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.Interact.performed += input => Interact(); // += adds a function delegate to the list of delegates called when, in this case, the interact button is pressed. The input => Interact() stuff is a lambda expression.
    }

    protected virtual void Start()
    {
        if (yarnScriptToLoad != null)
            dialogueRunner.Add(yarnScriptToLoad);

        player = GameObject.Find("Player");
    }

    protected virtual void Update()
    {
        if (this.GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>()))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!dialogueRunner.IsDialogueRunning)
                    dialogueRunner.StartDialogue(startNode);
                else
                    dialogueUI.MarkLineComplete();
            }
        }
    }

    protected virtual void Interact()
    {
        if (this.GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>()))
        {
            if (!dialogueRunner.IsDialogueRunning)
                dialogueRunner.StartDialogue(startNode);
            else
                dialogueUI.MarkLineComplete();
        }
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
