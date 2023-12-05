using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : Health
{
    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private Mover _mover;
    [SerializeField] private int _reward;

    private bool _isAngry = false;

    public int Reward => _reward;
    public bool IsAngry => _isAngry;

    protected override void OnEnable()
    {
        ChangeHealthMultiplier();
        base.OnEnable();
        _mover.SetStartSpeed();
        _isAngry = false;
    }
    private void Update()
    {
        if (transform.position.y < 0)
        {
            gameObject.SetActive(false);
        }
    }
    public override void Die()
    {
        _mover.SetNoSpeed();
        base.Die();
        Debug.Log("died");

    }

    private void ChangeHealthMultiplier()
    {
        AddHealthMultiplier = _levelChanger.CurrentLevelNumber / _levelChanger.BossLevelNumber;
    }

    public void SetAngry()
    {
        _isAngry = true;
    }
}
