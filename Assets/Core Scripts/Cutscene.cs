using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Cutscene : MonoBehaviour
{
    private PlayableDirector playableDirector;
    private Animator playerAnimator;

    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
            playableDirector.Play();
    }

    public void setMoveable(bool input)
    {
        playerAnimator.SetBool("canMove", input);
    }
}
