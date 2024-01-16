using UnityEngine;

namespace Truck
{
    public class JumpTrigger : MonoBehaviour
    {
        [SerializeField] private Transform _jumpPoint;

        public Transform JumpPoint => _jumpPoint;
    }
}