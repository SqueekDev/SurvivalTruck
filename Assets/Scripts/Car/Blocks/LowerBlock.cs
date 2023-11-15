using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBlock : WoodBlock
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Obstacle.LowerBlockDestroyed += OnBlockDestroyed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Obstacle.LowerBlockDestroyed -= OnBlockDestroyed;
    }
}
