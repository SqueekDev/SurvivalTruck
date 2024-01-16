using Base;
using UnityEngine;

namespace UI
{
    public class LostPanelEnabler : MonoBehaviour
    {
        [SerializeField] private Health _playerHealth;
        [SerializeField] private GamePanel _lostPanel;

        private void OnEnable()
        {
            _playerHealth.Died += OnPlayerDied;
        }

        private void OnDisable()
        {
            _playerHealth.Died -= OnPlayerDied;            
        }

        private void OnPlayerDied(Health player)
        {
            _lostPanel.gameObject.SetActive(true);
        }
    }
}