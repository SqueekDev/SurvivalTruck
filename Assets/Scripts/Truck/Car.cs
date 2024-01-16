using UnityEngine;

namespace Truck
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public float Speed => _speed;

        private void FixedUpdate()
        {
            transform.Translate(Vector3.forward * _speed * Time.fixedDeltaTime);
        }
    }
}