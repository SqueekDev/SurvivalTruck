using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Health
{
    [SerializeField] private Mover _mover;
    [SerializeField] private string _savingName;

    protected override void OnEnable()
    {
        if (_savingName != null)
            AddHealthMultiplier = PlayerPrefs.GetInt(_savingName, 0);

        base.OnEnable();
        _mover.SetStartSpeed();
    }

    protected override void Die()
    {
        _mover.SetNoSpeed();
        base.Die();
    }

    private void UpgradeHealth()
    {
        AddHealthMultiplier++;
        PlayerPrefs.SetInt(_savingName, AddHealthMultiplier);
        ChangeMaxHealth();
    }
}
