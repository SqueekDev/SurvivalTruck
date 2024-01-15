using Base;
using UnityEngine;

namespace Enemy
{
    public class FallDistanceTransition : Transition
    {
        [SerializeField] private PushBackState _pushBackState;

        protected override void OnEnable()
        {
            base.OnEnable();
            _pushBackState.Fell += OnFell;
        }

        private void OnDisable()
        {
            _pushBackState.Fell -= OnFell;
        }

        private void OnFell()
        {
            NeedTransit = true;
        }
    }
}
