using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushBackState : JumpingOnCarState
{
    private const string FallingBoolName = "Fall";

    public event UnityAction Fell;

    protected override void OnEnable()
    {
        BossAnimator.SetBool(FallingBoolName, true);
    }

    protected override void OnDisable()
    {
        BossAnimator.SetBool(FallingBoolName, false);
    }

    private void Update()
    {
        if (Target.transform.position.z - transform.position.z > ZOffset)
            Fell?.Invoke();
    }
}
