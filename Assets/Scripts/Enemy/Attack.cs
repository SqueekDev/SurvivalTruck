using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour
{
    private Zombie _zombie;
    private ZombieAttacker _zombieAttacker;

    private Transform _target;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombie = animator.gameObject.GetComponent<Zombie>();
        _zombieAttacker=animator.gameObject.GetComponent<ZombieAttacker>();

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _target = _zombie.GetTarget();
        if (_target.TryGetComponent(out Obstacle obstacle))
        {
            _zombieAttacker.Attack(obstacle);
        }
        if (_target.TryGetComponent(out Player player))
        {
            _zombieAttacker.Attack(player);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}