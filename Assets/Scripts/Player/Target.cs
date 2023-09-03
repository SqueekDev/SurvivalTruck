using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _size;

    private void LateUpdate()
    {
        float scale = Vector3.Distance(transform.position, _cameraTransform.position);
        transform.localScale = Vector3.one * scale * _size;
    }
}
