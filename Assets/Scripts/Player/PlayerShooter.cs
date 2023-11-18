using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : Shooter
{
    [SerializeField] private UpgradesPanel _upgradesPanel;
    [SerializeField] private Shop _shop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Shop shop) && IsShooting == false)
        {
            StartCoroutine(CoverOpening());
        }
    }

    private IEnumerator CoverOpening()
    {
        _shop.Open();
        yield return new WaitForSeconds(1);
        _upgradesPanel.gameObject.SetActive(true);

    }
}
