using Base;
using UnityEngine;

namespace Enemy
{
    public class PushBackTransition : BossTransition
    {
        [SerializeField] private Health _head;

        private void OnEnable()
        {
            NeedTransit = false;
        }

        private void Update()
        {
            if (_head.IsDead)
            {
                NeedTransit = true;
            }
        }
    }
}
