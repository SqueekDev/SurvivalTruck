using UnityEngine;

public class ThrowingOut : StateMachineBehaviour
{
    private ZombieMover _zombieMover;
    private Zombie _zombie;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieMover = animator.gameObject.GetComponent<ZombieMover>();
        _zombie = animator.gameObject.GetComponent<Zombie>();
        _zombieMover.ThrowAway(_zombie.GetTarget());
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieMover.StopThrowingAway();
    }
}
