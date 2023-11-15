using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodPiece : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Quaternion _startRotation;
    [SerializeField] private Vector3 _startScale;
    [SerializeField] private Rigidbody _rigidbody;

    public Vector3 StartPosition => _startPosition;
    public Quaternion StartRotation => _startRotation;
    public Vector3 StartScale => _startScale;
    public Rigidbody Rigidbody => _rigidbody;
}
