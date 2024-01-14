using Truck;
using Player;
using UnityEngine;

namespace Enemy
{
    public class Attack : StateMachineBehaviour
    {
        [SerializeField] private float _attackDistance;

        private Zombie _zombie;
        private ZombieAttacker _zombieAttacker;
        private Transform _target;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _zombie = animator.gameObject.GetComponent<Zombie>();
            _zombieAttacker = animator.gameObject.GetComponent<ZombieAttacker>();
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _target = _zombie.GetTarget();

            if (Vector3.Distance(_zombie.transform.position, _target.position) < _attackDistance)
            {
                if (_target.TryGetComponent(out Obstacle obstacle))
                {
                    _zombieAttacker.Attack(obstacle);
                }

                if (_target.TryGetComponent(out PlayerHealth player))
                {
                    _zombieAttacker.Attack(player);
                }
            }
        }
    }
}
