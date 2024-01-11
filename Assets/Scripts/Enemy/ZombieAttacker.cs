using System;
using System.Collections;
using UnityEngine;

public class ZombieAttacker : MonoBehaviour
{
    private const float TimeBetweenAttacks = 1f;

    [SerializeField] private int _startDamage;
    [SerializeField] private LevelChanger _levelChanger;

    private Coroutine _obstacleAttacking;
    private Coroutine _attacking;
    private int _currentDamage;
    private int _damageModifier;
    private int _midDamageModifierValue = 2;
    private int _maxDamageModifierValue = 6;
    private float _rotationAngleX = 0f;
    private float _rotationAngleY = 90f;
    private float _rotationAngleZ = 0f;
    private bool _isAttacking = false;
    private WaitForSeconds _delayBetweenAttacks = new WaitForSeconds(TimeBetweenAttacks);

    public event Action OnObstacleDestroyed;

    public bool IsAttacking => _isAttacking;

    private void Awake()
    {
        _damageModifier = UnityEngine.Random.Range(_midDamageModifierValue, _maxDamageModifierValue);
    }

    private void OnEnable()
    {
        _isAttacking = false;
        _levelChanger.Changed += OnLevelChanged;
        OnLevelChanged(_levelChanger.CurrentLevelNumber);
    }

    private void OnDisable()
    {
        _levelChanger.Changed -= OnLevelChanged;
    }

    public void Attack(Obstacle obstacle)
    {
        if (obstacle.transform.parent.position.x > transform.position.x)
        {
            transform.localEulerAngles = new Vector3(_rotationAngleX, _rotationAngleY, _rotationAngleZ);
        }

        if (obstacle.transform.parent.position.x < transform.position.x)
        {
            transform.localEulerAngles = new Vector3(_rotationAngleX, -_rotationAngleY, _rotationAngleZ);
        }

        if (_obstacleAttacking == null)
        {
            _obstacleAttacking = StartCoroutine(Attacking(obstacle));
        }
    }

    public void Attack(PlayerHealth player)
    {
        transform.LookAt(player.transform);

        if (_attacking == null)
        {
            _attacking = StartCoroutine(Attacking(player));
        }
    }

    private IEnumerator Attacking(Obstacle obstacle)
    {
        _isAttacking = true;
        obstacle.ApplyDamade(_currentDamage);
        yield return _delayBetweenAttacks;

        if (obstacle.IsDestroyed)
        {
            OnObstacleDestroyed?.Invoke();
        }

        _obstacleAttacking = null;
        _isAttacking = false;
    }

    private IEnumerator Attacking(Health playerHealth)
    {
        _isAttacking = true;
        playerHealth.TakeDamage(_currentDamage);
        yield return _delayBetweenAttacks;
        _attacking = null;
        _isAttacking = false;
    }

    private void OnLevelChanged(int levelNumber)
    {
        _currentDamage = _startDamage + (levelNumber / _damageModifier);
    }
}
