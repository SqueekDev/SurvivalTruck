using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperBlock : WoodBlock
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Obstacle.UpperBlockDestroyed += OnBlockDestroyed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Obstacle.UpperBlockDestroyed -= OnBlockDestroyed;
    }
}
