using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Health
{
    [SerializeField] private Mover _mover;
    [SerializeField] private CameraLowerPoint _cameraLowerPoint;
    [SerializeField] private PlayerHealthUpgradeButton _playerHealthUpgradeButton;

    private Car _car;

    private void Awake()
    {
        _car = GetComponentInParent<Car>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _mover.SetStartSpeed();
        _playerHealthUpgradeButton.HealthUpgraded += ChangeMaxHealth;
    }

    private void OnDisable()
    {
        _playerHealthUpgradeButton.HealthUpgraded -= ChangeMaxHealth;        
    }

    protected override void Die()
    {
        Vector3 cameraLowerPointRotation = _cameraLowerPoint.transform.rotation.eulerAngles;
        _cameraLowerPoint.transform.parent = _car.transform;
        _cameraLowerPoint.transform.rotation = Quaternion.Euler(cameraLowerPointRotation);
        _mover.SetNoSpeed();
        base.Die();
    }

    protected override void ChangeMaxHealth()
    {
        MaxHealth = PlayerPrefs.GetInt(PlayerPrefsKeys.PlayerHealth, MaxHealth);
        Heal(MaxHealth);
    }
}
