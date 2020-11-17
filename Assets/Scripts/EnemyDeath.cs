using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2D;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetFloat("health") <= 0 && !animator.GetBool("dead"))
        {

            StartCoroutine(deathCoroutine());
        }
    }

    private IEnumerator deathCoroutine()
    {
        animator.SetTrigger("died");
        animator.SetBool("dead", true);

        // Set rigid body to static after death. 
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("dead"))
            yield return null; 

        rigidbody2D.bodyType = RigidbodyType2D.Static;
        animator.enabled = false;
    }

}
