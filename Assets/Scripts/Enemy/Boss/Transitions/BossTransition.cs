using UnityEngine;

public class BossTransition : MonoBehaviour
{
    [SerializeField] private BossState _targetState;

    public BossState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
