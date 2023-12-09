using System.Collections;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Scope _scope;
    [SerializeField] private Target _target;

    private Coroutine _shootCorutine;
    private Vector3 _targetStartPositon;

    private void OnEnable()
    {
        _scope.gameObject.SetActive(true);
        _target.gameObject.SetActive(true);
        CheckCorutine();
        _shootCorutine = StartCoroutine(Shooting());
    }

    private void Start()
    {
        _targetStartPositon = _target.transform.localPosition;
    }

    private void LateUpdate()
    {
        _target.transform.localPosition = _targetStartPositon;
    }

    private void OnDisable()
    {
        CheckCorutine();
        _scope.gameObject.SetActive(false);
        _target.gameObject.SetActive(false);
    }

    private IEnumerator Shooting()
    {
        WaitForSeconds delay = new WaitForSeconds(_weapon.TimeBetweenShoot);

        while (true)
        {
            _weapon.Shoot(_target.transform);
            yield return delay;
        }
    }

    private void CheckCorutine()
    {
        if (_shootCorutine != null)
            StopCoroutine(_shootCorutine);
    }
}
