using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : Mover
{
    public void Move(Vector3 direction)
    {
        transform.position = new Vector3(transform.position.x + direction.x * Time.deltaTime * Speed,
            transform.position.y, transform.position.z + direction.z * Time.deltaTime * Speed);
    }
}
