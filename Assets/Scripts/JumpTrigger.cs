using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    [SerializeField] private Transform _jumpPoint;

    public Transform JumpPoint => _jumpPoint;
}
