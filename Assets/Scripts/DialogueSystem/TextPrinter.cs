using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Use namespace for avoiding name conflicts as project gets bigger
namespace DialogueSystem
{
    public class TextPrinter : MonoBehaviour
    {
        private PlayerMovement playerMovement;

        private void Start()
        {
            playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }

        // input is what needs be printed, textHolder is what is currently shown in dialogue box
        // TODO: maybe an if statement to search for \n to start new line
        protected IEnumerator WriteText(string[] dialogueLines, GameObject dialogueHolder, Text textHolder, float printDelay)
        {
            playerMovement.inDialogue = true;
            dialogueHolder.SetActive(true);

            for (int lineIdx = 0; lineIdx < dialogueLines.Length; lineIdx++)
            {
                textHolder.text = ""; // Reset text in text object,
                for (int i = 0; i < dialogueLines[lineIdx].Length; i++)
                {
                    // Add new character to text object every printDelay
                    textHolder.text += dialogueLines[lineIdx][i];
                    //yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                    yield return new WaitForSeconds(printDelay);
                }
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            }

            playerMovement.inDialogue = false;
            dialogueHolder.SetActive(false);
        }
    }
}

