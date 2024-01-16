using Player;
using Truck;
using UnityEngine;

namespace Enemy
{
    public class Zombie : MonoBehaviour
    {
        private const string AttackDistance = "attackDistance";
        private const string MoveTo = "MoveTo";
        private const string Jump = "Jump";
        private const string Kangaroo = "Kangaroo";

        [SerializeField] private ZombieJumper _zombieJumper;
        [SerializeField] private ZombieAttacker _zombieAttacker;
        [SerializeField] private ZombieHealth _zombieHealth;
        [SerializeField] private PlayerHealth _player;
        [SerializeField] private AudioSource _kangarooCollision;

        private Transform _target;
        private Obstacle _obstacle;
        private Animator _animator;
        private bool _firstRageAreaCollision = true;

        private void OnEnable()
        {
            _zombieAttacker.OnObstacleDestroyed += OnOstacleDestroyed;
            _firstRageAreaCollision = true;
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

            if (_target.TryGetComponent(out Obstacle obstacle))
            {
                Vector3 newPosition = new Vector3(_target.transform.position.x, _target.transform.position.y, transform.position.z);
                _animator.SetFloat(AttackDistance, Vector3.Distance(transform.position, newPosition));
            }
            else
            {
                _animator.SetFloat(AttackDistance, Vector3.Distance(transform.position, _target.position));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_firstRageAreaCollision && other.TryGetComponent(out RageArea rageArea))
            {
                _firstRageAreaCollision = false;
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
            if (collision.gameObject.TryGetComponent(out CarShield kangaroo))
            {
                _animator.SetTrigger(Kangaroo);
                _kangarooCollision.Play();
            }
        }

        public void SetObstacle(Obstacle obstacle)
        {
            _obstacle = obstacle;
        }

        public Transform GetTarget()
        {
            if (_obstacle != null && _obstacle.IsDestroyed == false)
            {
                SetTarget(_obstacle.transform);
            }
            else
            {
                SetTarget(_player.transform);
            }

            return _target;
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