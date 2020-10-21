using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        private void Start()
        {
            //StartCoroutine(dialogueSequence());
        }

        /*
        private IEnumerator dialogueSequence()
        {
            Deactivate();
            transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitUntil(() => transform.GetChild(0).GetComponent<DialogueLines>().lineFinished);

            gameObject.SetActive(false); // Set Dialopgue holder to false after final dialogue line is done
        }
        */

        private void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}