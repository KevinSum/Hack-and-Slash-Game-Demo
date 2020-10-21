using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    // This script should be attached to NPCs or Objects that need to have dialogue when interacted with.
    public class DialogueLines : MonoBehaviour
    {
        [SerializeField]
        public GameObject dialogueHolder;
        private GameObject NPC_DialogueHolderUI;
        private GameObject objectDialogueHolderUI;
        private Text textbox;

        private Image imageHolder;
        private GameObject NPC_nameHolder;
        private Text NPC_nameTextbox;
        private GameObject player;
        private PlayerMovement playerMovement;

        // These are to be set on a individual basis, depending on dialogue and character etc
        [Header("Text Options")]
        [SerializeField] private string NPC_name; // Name of NPC
        [SerializeField] private string[] dialogueLines; // Dialogue lines

        [Header("Time Parameters")]
        [SerializeField] private float printDelay;

        [Header("Character Image")]
        [SerializeField] private Sprite characterSprite;
        

        private void Awake() // Do before game starts
        {
            // These assignments assume that there is a gameObject called "UI", and which there is a child "NPC dialogue" gameObject, and of which there is a "Character Image" gameObject etc.
            dialogueHolder = GameObject.Find("UI").transform.Find("Dialogue Holder").gameObject;
            NPC_DialogueHolderUI = dialogueHolder.transform.Find("NPC Dialogue UI").gameObject;
            objectDialogueHolderUI = dialogueHolder.transform.Find("Object Dialogue UI").gameObject;

            // NPC dialogue have extra UI elements (sprite image and name)
            if (transform.tag == "NPC")
            {
                imageHolder = NPC_DialogueHolderUI.transform.Find("Character Image").GetComponent<Image>();
                NPC_nameHolder = NPC_DialogueHolderUI.transform.Find("Name Holder").gameObject;
                NPC_nameTextbox = NPC_nameHolder.transform.Find("Name Textbox").GetComponent<Text>();
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
                    if (1==1)
                    {
                        //playerMovement.inDialogue = true;

                        // Set appropriate textboxes and UIs
                        if (transform.tag == "Object")
                        {
                            NPC_DialogueHolderUI.SetActive(false);
                            objectDialogueHolderUI.SetActive(true);
                            textbox = objectDialogueHolderUI.GetComponent<Text>();
                        }
                        else if (transform.tag == "NPC")
                        {
                            // NPC has extra stuff to set (Names and sprite image to place alongside text)
                            imageHolder.sprite = characterSprite;
                            imageHolder.preserveAspect = true;
                            NPC_nameTextbox.text = NPC_name;
                            NPC_nameHolder.SetActive(true);

                            NPC_DialogueHolderUI.SetActive(true);
                            objectDialogueHolderUI.SetActive(false);
                            textbox = NPC_DialogueHolderUI.GetComponent<Text>();
                        }
                        StartCoroutine(WriteText(dialogueLines, textbox, printDelay)); // print text
                    }
                }
            }
        }

        protected IEnumerator WriteText(string[] dialogueLines, Text textBox, float printDelay)
        {
            dialogueHolder.SetActive(true); 
            for (int lineIdx = 0; lineIdx < dialogueLines.Length; lineIdx++)
            {
                textBox.text = ""; // Reset text in text object,
                for (int i = 0; i < dialogueLines[lineIdx].Length; i++)
                {
                    // Add new character to text object every printDelay
                    textBox.text += dialogueLines[lineIdx][i];
                    yield return new WaitForSeconds(printDelay);
                }
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            }
            dialogueHolder.SetActive(false);
            //playerMovement.inDialogue = false;
        }

    } 
}