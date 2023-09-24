using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private const int Doubler = 2;

    [SerializeField] private int _maxHealth;
    [SerializeField] private List<WoodBlock> _blocks;
    [SerializeField] private RepairZone _repairZone;

    private int _currentHealth;
    private bool _upperBlockEnabled;
    private bool _middleBlockEnabled;

    public bool IsDestroyed { get; private set; }

    private void Awake()
    {
        _currentHealth = _maxHealth;
        IsDestroyed = false;
        _upperBlockEnabled = true;
        _middleBlockEnabled = true;
    }

    private void OnEnable()
    {
        _repairZone.Repaired += OnRepaired;
    }

    private void OnDisable()
    {
        _repairZone.Repaired -= OnRepaired;        
    }

    public void ApplyDamade(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth < _maxHealth / _blocks.Count * Doubler && _upperBlockEnabled)
        {
            DisactiveBlock();
            _upperBlockEnabled = false;
        }    

        if (_currentHealth < _maxHealth / _blocks.Count && _middleBlockEnabled)
        {
            DisactiveBlock();
            _middleBlockEnabled = false;
        }    

        if (_currentHealth <= 0)
        {
            foreach (var block in _blocks)
                block.gameObject.SetActive(false);

            _currentHealth = 0;
            IsDestroyed = true;
        }    
    }

    private void DisactiveBlock()
    {
        foreach (var block in _blocks)
        {
            if (block.gameObject.activeInHierarchy)
            {
                block.gameObject.SetActive(false);
                break;
            }
        }
    }

    private void OnRepaired()
    {
        foreach (var block in _blocks)
            block.gameObject.SetActive(true);

        _currentHealth = _maxHealth;
        IsDestroyed = false;
        _upperBlockEnabled = true;
        _middleBlockEnabled = true;
    }
}
