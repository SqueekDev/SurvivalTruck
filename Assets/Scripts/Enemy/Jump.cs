using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateMachineBehaviour
{
    private ZombieMover _zombieMover;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieMover = animator.gameObject.GetComponent<ZombieMover>();
        _zombieMover.Jump();
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieMover.StopJump();
    }
}
