using UnityEngine;

namespace Base
{
    public abstract class Mover : MonoBehaviour
    {
        [SerializeField] protected float StartSpeed;
        [SerializeField] protected float Speed;

        protected Rigidbody Rigidbody;

        [SerializeField] private float _rotationSpeed;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
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

        public void SetZeroSpeed()
        {
            Speed = GlobalValues.Zero;
        }

        public void SetStartSpeed()
        {
            Speed = StartSpeed;
        }
    }
}