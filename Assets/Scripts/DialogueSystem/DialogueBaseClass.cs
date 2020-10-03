using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Use namespace for avoiding name conflicts as project gets bigger
namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool lineFinished { get; private set; } // private setter, public getter
        
        // input is what needs be printed, textHolder is what is currently shown in dialogue box
        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float printDelay)
        {
            textHolder.font = textFont;
            textHolder.color = textColor;
            

            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(printDelay);
            }
            yield return new WaitUntil(() => Input.GetMouseButton(0));

            lineFinished = true;
        }
    }
}

