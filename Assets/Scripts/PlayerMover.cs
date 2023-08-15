using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : Mover
{
    [SerializeField] private UpgradesPanel _upgradesPanel;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Shop shop))
        {
            _upgradesPanel.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Shop shop))
        {
            _upgradesPanel.gameObject.SetActive(false);

        }
    }

    public void Move(Vector3 direction)
    {
        _rigidbody.velocity = new Vector3(direction.x * _speed,
            _rigidbody.velocity.y, direction.z * _speed);
    }
}
