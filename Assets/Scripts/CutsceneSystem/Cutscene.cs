using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Cutscene : MonoBehaviour
{
    private PlayableDirector playableDirector;
    private Animator playerAnimator;
    private BoxCollider2D boxCollider2D;

    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
            playableDirector.Play();
    }

    public void SetInCutscene(bool input)
    {
        playerAnimator.SetBool("canMove", !input);
        playerAnimator.SetBool("inCutscene", input);
    }

    public void disableTrigger()
    {
        boxCollider2D.enabled = false;
    }

}
