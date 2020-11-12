using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

// Extra a dialogue functionalities for Yarn Dialogue System.
// AutoContinue - Automatically continue onto next dialogue. Helpful for continuing onto options without user having to press continue 
// visited(nodeName) - Check if node has been visited
public class ExtraDialogueCommands : MonoBehaviour
{
    DialogueUI dialogueUI;
    DialogueRunner dialogueRunner;

    private bool autoContinue;
    private HashSet<string> _visitedNodes = new HashSet<string>();
    // Start is called before the first frame update
    void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
        dialogueRunner = GetComponent<DialogueRunner>();

        dialogueRunner.AddCommandHandler("AutoContinue", (string[] s) => autoContinue = true); // Set autoContinue to true if there's an "AutoContinue" command in the yarn dialogue file
        dialogueUI.onLineFinishDisplaying.AddListener(AutoContinueCheck);

        // Register a function on startup called "visited" that lets Yarn
        // scripts query to see if a node has been run before.
        dialogueRunner.AddFunction("visited", 1, delegate (Yarn.Value[] parameters)
        {
            var nodeName = parameters[0];
            Debug.Log(_visitedNodes.Contains(nodeName.AsString));
            return _visitedNodes.Contains(nodeName.AsString);
        });


    }

    // Go to next line if AutoContinue is true.
    void AutoContinueCheck() 
    {
        if (autoContinue)
        {
            dialogueUI.MarkLineComplete();
            autoContinue = false;
        }
    }

    // Called by the Dialogue Runner to notify us that a node finished
    // running. 
    public void NodeComplete(string nodeName)
    {
        // Log that the node has been run.
        _visitedNodes.Add(nodeName);
    }


}
