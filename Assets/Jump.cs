using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateMachineBehaviour
{
    private ZombieMover _zombieMover;
    private Zombie _zombie;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieMover=animator.gameObject.GetComponent<ZombieMover>();
        _zombie=animator.gameObject.GetComponent<Zombie>();
        _zombieMover.Jump(_zombie.GetTarget());
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieMover.StopJump();
    }
}
