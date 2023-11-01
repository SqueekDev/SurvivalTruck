using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : Shooter
{
    [SerializeField] private UpgradesPanel _upgradesPanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Shop shop) && IsShooting == false)
        {
            _upgradesPanel.gameObject.SetActive(true);
        }
    }
}
