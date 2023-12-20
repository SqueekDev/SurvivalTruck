using UnityEngine;

public class Jump : StateMachineBehaviour
{
    private ZombieMover _zombieMover;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieMover = animator.gameObject.GetComponent<ZombieMover>();
        _zombieMover.Jump();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieMover.StopJump();
    }
}
