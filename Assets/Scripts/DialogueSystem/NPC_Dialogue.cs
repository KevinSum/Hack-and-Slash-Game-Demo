﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class NPC_Dialogue : Dialogue
{
    [Header("NPC Info")]
    [SerializeField] private string NPC_name; // Name to be shown on UI
    [SerializeField] private Sprite characterSprite;

    private Text NPC_nameTextbox;
    private Image imageHolder;

    void Awake()
    {
        dialogueRunner = GameObject.Find("NPC Dialogue System").GetComponent<DialogueRunner>();
        dialogueUI = GameObject.Find("NPC Dialogue System").GetComponent<DialogueUI>();

        GameObject NPC_Dialogue_UI = GameObject.Find("UI").transform.Find("Dialogue Holder").Find("NPC Dialogue UI").gameObject;
        imageHolder = NPC_Dialogue_UI.transform.Find("Character Image").GetComponent<Image>();
        NPC_nameTextbox = NPC_Dialogue_UI.transform.Find("Name Holder").transform.Find("Name Textbox").GetComponent<Text>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); // Call Update() in parent class

        if (this.GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>()))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Set name and sprite image
                // Is there a way to do this through Yarn?
                NPC_nameTextbox.text = NPC_name;
                imageHolder.sprite = characterSprite;
                imageHolder.preserveAspect = true;
            }
        }
    }
}