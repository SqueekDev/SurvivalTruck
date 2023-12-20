using UnityEngine;

public class PlayerDiedTransition : BossTransition
{
    [SerializeField] private Player _player;

    private void Update()
    {
        if (_player.IsDead)
        {
            NeedTransit = true;
        }
    }
}
