using UnityEngine;

namespace Base
{
    public abstract class Mover : MonoBehaviour
    {
        [SerializeField] protected float StartSpeed;
        [SerializeField] protected float Speed;

        protected Rigidbody Rigidbody;

        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Health _health;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        protected virtual void OnEnable()
        {
            SetStartSpeed();
            _health.Died += OnDied;
        }

        protected virtual void OnDisable()
        {
            _health.Died -= OnDied;            
        }

        public void Rotate(Vector3 destination)
        {
            Vector3 direction = new Vector3(destination.x, transform.position.y, destination.z) - transform.position;
            Quaternion newDirection = transform.rotation;

            if (direction != Vector3.zero)
            {
                newDirection = Quaternion.LookRotation(direction);

            }

            transform.rotation = Quaternion.Lerp(transform.rotation, newDirection, _rotationSpeed * Time.deltaTime);
        }

        private void SetZeroSpeed()
        {
            Speed = GlobalValues.Zero;
        }

        private void SetStartSpeed()
        {
            Speed = StartSpeed;
        }

        private void OnDied(Health health)
        {
            SetZeroSpeed();
        }
    }
}