using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Kangaroo : Cabine
{
    [SerializeField] private int _damage;
    [SerializeField] private KangarooDamageUpgradeButton _kangarooDamageUpgradeButton;
    [SerializeField] private ParticleSystem _poofKangarooPartical;
    [SerializeField] private float _applyingDamageDelay=0.5f;

    public int Damage => _damage;

    public event UnityAction<ZombieHealth> ZombieHited;

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
        if (collision.gameObject.TryGetComponent(out ZombieHealth zombie))
        {
            ZombieHited?.Invoke(zombie);
            StartCoroutine(ApplyingDamage(zombie));
            _poofKangarooPartical.transform.position = zombie.transform.position;
            _poofKangarooPartical.Play();
        }
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
