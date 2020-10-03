using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    // For detecting if player is touching NPC, only then can dialogue be triggered
    private BoxCollider2D npcBoxCollider;
    private BoxCollider2D playerBoxCollider;

    // Dialogue stuff
    public GameObject dialogueBox; // Dialogue box, NOT THE TEXT
    public Text dialogueTextObject;
    public Queue<string> dialogueToPrint; // Queue used for FIFO
    public bool dialogueActive;



    // Start is called before the first frame update
    void Start()
    {
        playerBoxCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        npcBoxCollider = GetComponent<BoxCollider2D>();

        dialogueToPrint = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (npcBoxCollider.IsTouching(playerBoxCollider))
        {
            Debug.Log("Can trigger dialogue now");
        }
    }
}