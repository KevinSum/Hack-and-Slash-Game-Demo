using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    // This script should be attached to NPCs or Objects that need to have dialogue when interacted with.
    public class DialogueLines : TextPrinter
    {
        private Text textHolder;
        private Image imageHolder;
        private GameObject player;
        private PlayerMovement playerMovement;
        private GameObject dialogueHolder;

        // These are to be set on a individual basis, depending on dialogue and character etc
        [Header("Text Options")]
        [SerializeField] private string name; // Name of NPC
        [SerializeField] private string[] dialogueLines; // Dialogue lines

        [Header("Time Parameters")]
        [SerializeField] private float printDelay;

        [Header("Character Image")]
        [SerializeField] private Sprite characterSprite;
        

        private void Awake() // Do before game starts
        {
            // These assignments assume that there is a gameObject called "UI", and which there is a child "NPC dialogue" gameObject, and of which there is a "Character Image" gameObject.
            dialogueHolder = GameObject.Find("UI").transform.Find("Dialogue Holder").gameObject;
            textHolder = dialogueHolder.transform.Find("NPC Dialogue").GetComponent<Text>();
            textHolder.text = ""; // Get rid of text if there's placeholder text
            imageHolder = textHolder.transform.Find("Character Image").GetComponent<Image>();
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();

            // Maybe put in if statement, depending if speaking to a character or to an object (Which won't have an image). Maybe check tag? NPC or object
            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true; 
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!playerMovement.inDialogue)
                    StartCoroutine(WriteText(dialogueLines, dialogueHolder, textHolder, printDelay)); // Maybe have in own function
            }
        }



    } 
}