using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private Text textHolder;

        [Header ("Text Options")]
        [SerializeField] private string input;
        [SerializeField] private Color textColor;
        [SerializeField] private Font textFont;

        [Header("Time Parameters")]
        [SerializeField] private float printDelay;

        [Header("Character Image")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Image imageHolder;

        private void Awake() // Do before game starts
        {
            // Get 'Text' component of object the script is attached to. This should be the text object inside the dialogue box object
            textHolder = GetComponent<Text>();
            textHolder.text = ""; // Get rid of text if there's placeholder text

            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }

        private void Start() // Do when update method called for first time
        {
            StartCoroutine(WriteText(input, textHolder, textColor, textFont, printDelay));
        }
    } 
}