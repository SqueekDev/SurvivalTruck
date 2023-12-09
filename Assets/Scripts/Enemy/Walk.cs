using UnityEngine;

public class Walk : StateMachineBehaviour
{
    private ZombieMover _zombieMover;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieMover = animator.gameObject.GetComponent<ZombieMover>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieMover.MoveForward();
    }
}
