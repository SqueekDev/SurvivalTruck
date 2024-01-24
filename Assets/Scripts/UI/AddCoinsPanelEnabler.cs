using UnityEngine;

namespace UI
{
    public class AddCoinsPanelEnabler : MonoBehaviour
    {
        [SerializeField] private GameButton _addCoinsButton;
        [SerializeField] private GamePanel _addCoinsPanel;

        private void OnEnable()
        {
            _addCoinsButton.Clicked += OnAddCoinsButtonClicked;
        }

        private void OnDisable()
        {
            _addCoinsButton.Clicked -= OnAddCoinsButtonClicked;
        }

        private void OnAddCoinsButtonClicked()
        {
            _addCoinsPanel.gameObject.SetActive(true);
        }
    }
}