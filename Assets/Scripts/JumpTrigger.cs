using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    [SerializeField] private Transform _jumpPoint;

    public Transform JumpPoint => _jumpPoint;
}
