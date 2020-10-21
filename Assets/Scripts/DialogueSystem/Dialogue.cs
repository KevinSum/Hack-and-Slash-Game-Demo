using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Dialogue : MonoBehaviour
{
    public string characterName = ""; // Name to be shown on UI
    public string startNode = ""; // Name of starting node
    public YarnProgram yarnScriptToLoad; // Yarn script to be used (This is optional, as a pre-loaded script could also be used 

    private DialogueRunner dialogueRunner;
    private DialogueUI dialogueUI;
    private GameObject player;
    private PlayerMovement playerMovement;

    // Update is called once per frame
    void Start()
    {
        dialogueRunner = GameObject.Find("NPC Dialogue System").GetComponent<DialogueRunner>();
        dialogueUI = GameObject.Find("NPC Dialogue System").GetComponent<DialogueUI>();
        if (yarnScriptToLoad != null)
        {
            dialogueRunner.Add(yarnScriptToLoad);
        }

        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (this.GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>()))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!dialogueRunner.IsDialogueRunning)
                {
                    dialogueRunner.StartDialogue(startNode);
                }
                else
                {
                    dialogueUI.MarkLineComplete();
                }
                 

            }
        }
    }



    private void setInDialogue()
    {
        Debug.Log("Here");
    }
}
