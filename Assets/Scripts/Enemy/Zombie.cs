using Base;
using Player;
using Truck;
using UnityEngine;

namespace Enemy
{
    public class Zombie : PoolObject
    {
        private const string AttackDistance = "attackDistance";
        private const string MoveTo = "MoveTo";
        private const string Jump = "Jump";
        private const string CarShield = "CarShield";

        [SerializeField] private ZombieJumper _zombieJumper;
        [SerializeField] private ZombieAttacker _zombieAttacker;
        [SerializeField] private ZombieHealth _zombieHealth;
        [SerializeField] private PlayerHealth _player;
        [SerializeField] private AudioSource _carShieldCollision;

        private Transform _target;
        private Obstacle _obstacle;
        private Animator _animator;
        private bool _isAttacking;

        private void OnEnable()
        {
            _zombieAttacker.OnObstacleDestroyed += OnOstacleDestroyed;
            _isAttacking = false;
        }

        private void OnDisable()
        {
            _zombieAttacker.OnObstacleDestroyed -= OnOstacleDestroyed;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            SetTarget(_player.transform);
        }

        private void Update()
        {
            _target = GetTarget();
            Vector3 newPosition;

            if (_target.TryGetComponent(out Obstacle obstacle))
            {
                newPosition = new Vector3(_target.transform.position.x, _target.transform.position.y, transform.position.z);
            }
            else
            {
                newPosition = _target.position;
            }

            _animator.SetFloat(AttackDistance, Vector3.Distance(transform.position, newPosition));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isAttacking == false && other.TryGetComponent(out RageArea rageArea))
            {
                _isAttacking = true;
                SetObstacle(rageArea.Obstacle);
                _animator.SetTrigger(MoveTo);
                _zombieHealth.SetAngry();
                rageArea.Attacked(_zombieHealth);
                transform.SetParent(rageArea.transform.parent);
            }

            if (other.TryGetComponent(out JumpTrigger jumpTrigger))
            {
                _zombieJumper.SetJumpPoint(jumpTrigger.JumpPoint);
                _animator.SetTrigger(Jump);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CarShield carShield))
            {
                _animator.SetTrigger(CarShield);
                _carShieldCollision.Play();
            }
        }

        public Transform GetTarget()
        {
            Transform target;

            if (_obstacle != null && _obstacle.IsDestroyed == false)
            {
                target = _obstacle.transform;
            }
            else
            {
                target = _player.transform;
            }

            SetTarget(target);
            return _target;
        }

        private void SetObstacle(Obstacle obstacle)
        {
            _obstacle = obstacle;
        }

        private void OnOstacleDestroyed()
        {
            SetTarget(_player.transform);
        }

        private void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}