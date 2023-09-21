using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Target _target;
    [SerializeField] private float _timeBetweenShoot;

    private Coroutine _shootCorutine;
    private Vector3 _targetStartPositon;

    private void OnEnable()
    {
        _target.gameObject.SetActive(true);
        CheckCorutine();
        _shootCorutine = StartCoroutine(Shooting());
    }

    private void Start()
    {
        _targetStartPositon = _target.transform.position;
    }

    private void LateUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            _target.transform.position = hit.point;
        else
            _target.transform.position = _targetStartPositon;
    }

    private void OnDisable()
    {
        CheckCorutine();
        _target.gameObject.SetActive(false);
    }

    private IEnumerator Shooting()
    {
        WaitForSeconds delay = new WaitForSeconds(_timeBetweenShoot);

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
