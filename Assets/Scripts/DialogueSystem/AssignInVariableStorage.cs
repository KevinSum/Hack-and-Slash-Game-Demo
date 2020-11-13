using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

// Assign Variable Storage contained in Player gameObject (which should not be destroyed during scene transition) 
// for Yarn Dialogue Runner through script, just so that I don't have to do it in inspector.
public class AssignInVariableStorage : MonoBehaviour
{
    DialogueRunner dialogueRunner;
    GameObject player;

    void Awake()
    {
        dialogueRunner = GetComponent<DialogueRunner>();
        player = GameObject.Find("Player");
        dialogueRunner.variableStorage = player.GetComponent<InMemoryVariableStorage>();
    }

}
