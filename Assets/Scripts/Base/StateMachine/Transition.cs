using UnityEngine;

namespace Base
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] private State _targetState;

        public State TargetState => _targetState;

        public bool NeedTransit { get; protected set; }

        protected virtual void OnEnable()
        {
            NeedTransit = false;
        }
    }
}