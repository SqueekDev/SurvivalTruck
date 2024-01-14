using UnityEngine;

namespace Enemy
{
    public class Walk : StateMachineBehaviour
    {
        private ZombieMover _zombieMover;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _zombieMover = animator.gameObject.GetComponent<ZombieMover>();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _zombieMover.MoveForward();
        }
    }
}
