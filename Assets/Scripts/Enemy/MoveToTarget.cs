using UnityEngine;

public class MoveToTarget : StateMachineBehaviour
{
    private ZombieMover _zombieMover;
    private Zombie _zombie;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieMover = animator.gameObject.GetComponent<ZombieMover>();
        _zombie = animator.gameObject.GetComponent<Zombie>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieMover.MoveTo(_zombie.GetTarget());
    }
}
