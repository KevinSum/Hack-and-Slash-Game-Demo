using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

// Print dialogue specifically into the NPC dialogue UI, as well as displaying name and character sprite
public class NPC_Dialogue : Dialogue
{
    [Header("NPC Info")]
    [SerializeField] private string NPC_name; // Name to be shown on UI
    [SerializeField] private Sprite characterSprite;

    private Text NPC_nameTextbox;
    private Image imageHolder;

    protected override void Start()
    {
        dialogueRunner = GameObject.Find("NPC Dialogue System").GetComponent<DialogueRunner>();
        dialogueUI = GameObject.Find("NPC Dialogue System").GetComponent<DialogueUI>();

        GameObject NPC_Dialogue_UI = GameObject.Find("UI").transform.Find("Dialogue Holder").Find("NPC Dialogue UI").gameObject;
        imageHolder = NPC_Dialogue_UI.transform.Find("Character Image").GetComponent<Image>();
        NPC_nameTextbox = NPC_Dialogue_UI.transform.Find("Name Holder").transform.Find("Name Textbox").GetComponent<Text>();

        base.Start();
    }

    // Update is called once per frame
    protected override void Interact()
    {
        base.Interact(); // Call Update() in parent class

        // Set name and sprite image
        // Is there a way to do this through Yarn?
        NPC_nameTextbox.text = NPC_name;
        imageHolder.sprite = characterSprite;
        imageHolder.preserveAspect = true;
    }

}