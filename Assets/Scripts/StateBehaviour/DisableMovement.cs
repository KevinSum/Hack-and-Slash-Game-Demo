using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableMomvement : StateMachineBehaviour
{
    PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("startAttack", false);
        animator.SetBool("queueAttack1", false);
        playerMovement.movementEnabled(false);

    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMovement.movementEnabled(true);
    }

}
