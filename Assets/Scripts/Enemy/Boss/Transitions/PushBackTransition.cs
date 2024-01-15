using Base;
using UnityEngine;

namespace Enemy
{
    public class PushBackTransition : Transition
    {
        [SerializeField] private Health _head;

        private void Update()
        {
            if (_head.IsDead)
            {
                NeedTransit = true;
            }
        }
    }
}
