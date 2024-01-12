using UnityEngine;

public class Jump : StateMachineBehaviour
{
    private ZombieJumper _zombieJumper;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieJumper = animator.gameObject.GetComponent<ZombieJumper>();
        _zombieJumper.Jump();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieJumper.StopJump();
    }
}
