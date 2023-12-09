using UnityEngine;

public class Player : Health
{
    [SerializeField] private Mover _mover;
    [SerializeField] private CameraLowerPoint _cameraLowerPoint;
    [SerializeField] private PlayerHealthUpgradeButton _playerHealthUpgradeButton;
    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private HealthBar _bossLevelHealthBar;
    [SerializeField] private PlayerHealthBar _normalLevelHealthBar;

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
        _levelChanger.Changed += OnLevelChanged;
        _levelChanger.BossLevelStarted += OnBossLevelStarted;
        _levelChanger.BossLevelEnded += OnBossLevelEnded;
    }

    private void OnDisable()
    {
        _playerHealthUpgradeButton.HealthUpgraded -= ChangeMaxHealth;        
        _levelChanger.Changed -= OnLevelChanged;
        _levelChanger.BossLevelStarted -= OnBossLevelStarted;
        _levelChanger.BossLevelEnded -= OnBossLevelEnded;
    }

    public override void Die()
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

    private void OnLevelChanged(int level)
    {
        ChangeMaxHealth();
    }

    private void OnBossLevelStarted()
    {
        _normalLevelHealthBar.gameObject.SetActive(false);
        _bossLevelHealthBar.gameObject.SetActive(true);
    }

    private void OnBossLevelEnded()
    {
        _bossLevelHealthBar.gameObject.SetActive(false);
        _normalLevelHealthBar.gameObject.SetActive(true);
    }
}
