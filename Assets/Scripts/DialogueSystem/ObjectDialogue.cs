using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ObjectDialogue : Dialogue
{
    // Start is called before the first frame update
    void Awake()
    {
        dialogueRunner = GameObject.Find("Object Dialogue System").GetComponent<DialogueRunner>();
        dialogueUI = GameObject.Find("Object Dialogue System").GetComponent<DialogueUI>();
    }
}
