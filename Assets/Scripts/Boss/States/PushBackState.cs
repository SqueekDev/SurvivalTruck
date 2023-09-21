using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushBackState : JumpingOnCarState
{
    public event UnityAction Fell;

    private void Update()
    {
        if (Target.transform.position.z - transform.position.z < ZOffset)
            Fell?.Invoke();
    }
}
