using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevelArea : MonoBehaviour
{
    [SerializeField] private GameButton _nextLevelButton;

    private void OnDisable()
    {
        ToggleButton(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Player player))
            ToggleButton(true);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Player player))
            ToggleButton(false);
    }

    private void ToggleButton(bool isActive)
    {
        _nextLevelButton.gameObject.SetActive(isActive);
    }
}
