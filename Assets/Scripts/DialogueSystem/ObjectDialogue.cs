using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

// Print dialogue specifically into NPC dialogue UI
public class ObjectDialogue : Dialogue
{
    // Start is called before the first frame update
    protected override void Start()
    {
        dialogueRunner = GameObject.Find("Object Dialogue System").GetComponent<DialogueRunner>();
        dialogueUI = GameObject.Find("Object Dialogue System").GetComponent<DialogueUI>();

        base.Start();
    }
}
