using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerState : BossState
{
    [SerializeField] private Player _target;
    [SerializeField] private float _speed;
    [SerializeField] private Health _head;
    [SerializeField] private BossHealthBar _headHealhBar;
    [SerializeField] private Collider _collider;

    private void OnEnable()
    {
        _collider.enabled = true;
        _headHealhBar.gameObject.SetActive(false);
        _head.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(_target.transform.position.x - transform.position.x, 0, _target.transform.position.z - transform.position.z).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);
    }
}
