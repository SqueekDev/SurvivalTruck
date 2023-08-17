using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesPanel : MonoBehaviour
{
    [SerializeField] private UpgradeButton[] _upgradeButtons;


    private void OnEnable()
    {

        StartCoroutine(Renewing());

    }


    private IEnumerator Renewing()
    {
        Time.timeScale = 0;
        yield return new WaitForSeconds(0.2f);
        foreach (var button in _upgradeButtons)
        {
            button.Renew();
        }
    }

    public void TurnOffPanel()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
