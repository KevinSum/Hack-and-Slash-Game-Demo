using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Dialogue : MonoBehaviour
{
    [Header("Yarn Spinner Stuff")]
    [SerializeField] protected DialogueRunner dialogueRunner;
    [SerializeField] protected DialogueUI dialogueUI;
    [SerializeField] private string startNode = ""; // Name of starting node
    [SerializeField] private YarnProgram yarnScriptToLoad; // Yarn script to be used (This is optional, as a pre-loaded script could also be used

    protected GameObject player;

    void Start()
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
}
