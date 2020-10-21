using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Not used. Now moved directly into dialogueLines. Though there may be a use for this in the future?

// Use namespace for avoiding name conflicts as project gets bigger
namespace DialogueSystem
{
    public class TextPrinter : MonoBehaviour
    {
        public bool isWriting;

        // input is what needs be printed, textHolder is what is currently shown in dialogue box
        // TODO: maybe an if statement to search for \n to start new line
        protected IEnumerator WriteText(string[] dialogueLines, Text textBox, float printDelay)
        {
            isWriting = true;
            for (int lineIdx = 0; lineIdx < dialogueLines.Length; lineIdx++)
            {
                textBox.text = ""; // Reset text in text object,
                for (int i = 0; i < dialogueLines[lineIdx].Length; i++)
                {
                    // Add new character to text object every printDelay
                    textBox.text += dialogueLines[lineIdx][i];
                    //yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                    yield return new WaitForSeconds(printDelay);
                }
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            }
            isWriting = false;
        }
    }
}

