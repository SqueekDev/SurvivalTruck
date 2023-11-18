using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kangaroo : Cabine
{
    [SerializeField] private int _damage;
    [SerializeField] private KangarooDamageUpgradeButton _kangarooDamageUpgradeButton;
    [SerializeField] private ParticleSystem _poofKangarooPartical;
    [SerializeField] private float _applyingDamageDelay=0.5f;

    public int Damage => _damage;

    private void Awake()
    {
        OnDamageUpgraded();
    }

    private void OnEnable()
    {
        _kangarooDamageUpgradeButton.DamageUpgraded += OnDamageUpgraded;
    }

    private void OnDisable()
    {
        _kangarooDamageUpgradeButton.DamageUpgraded -= OnDamageUpgraded;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.TryGetComponent(out Health health))
            StartCoroutine(ApplyingDamage(health));
        _poofKangarooPartical.transform.position = health.transform.position;
        _poofKangarooPartical.Play();
    }

    private IEnumerator ApplyingDamage(Health health)
    {

        yield return new WaitForSeconds(_applyingDamageDelay);
        health.TakeDamage(_damage);
    }

    private void OnDamageUpgraded()
    {
        _damage = PlayerPrefs.GetInt(PlayerPrefsKeys.KangarooDamage, _damage);
    }
}
