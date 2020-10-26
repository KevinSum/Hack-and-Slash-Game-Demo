using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : playerControls
{
    private Animator animator;
    private bool attacking = false;

    protected override void Awake()
    {
        base.Awake();
        controls.Player.Attack.performed += input => StartCoroutine(AttackCo());
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AttackCo()
    {
        
        animator.SetBool("attacking", true);
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);

        animator.SetBool("attacking", false);



    }
}
