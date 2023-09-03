using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrelMover : Mover
{
    [SerializeField] private Shooter _shooter;

    private void Update()
    {
        if (_shooter.IsShooting)
        {
            Rotate(_shooter.Target.transform.position);
        }
    }
}
