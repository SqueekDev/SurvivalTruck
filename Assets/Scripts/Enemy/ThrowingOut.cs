using UnityEngine;

public class ThrowingOut : StateMachineBehaviour
{
    private ZombieAwayThrower _zombieAwayThrower;
    private Zombie _zombie;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieAwayThrower = animator.gameObject.GetComponent<ZombieAwayThrower>();
        _zombie = animator.gameObject.GetComponent<Zombie>();
        _zombieAwayThrower.ThrowAway(_zombie.GetTarget());
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieAwayThrower.StopThrowingAway();
    }
}
