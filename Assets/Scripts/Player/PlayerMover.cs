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
        transform.position = new Vector3(transform.position.x + direction.x * Time.deltaTime * Speed,
            transform.position.y, transform.position.z + direction.z * Time.deltaTime * Speed);
    }
}
