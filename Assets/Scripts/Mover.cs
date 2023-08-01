using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    public void Rotate(Vector3 destination)
    {
        Vector3 direction = new Vector3(destination.x, transform.position.y, destination.z) - transform.position;
        Quaternion newDirection = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newDirection, _rotationSpeed * Time.deltaTime);
    }
}
