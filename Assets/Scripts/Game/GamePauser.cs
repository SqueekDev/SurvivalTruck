using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Game
{
    public class GamePauser : MonoBehaviour
    {
        [SerializeField] private List<GamePanel> _gamePanels;

        private void OnEnable()
        {
            foreach (var panel in _gamePanels)
            {
                panel.Opened += OnOpened;
                panel.Closed += OnClosed;
            }
        }

        private void OnDisable()
        {
            foreach (var panel in _gamePanels)
            {
                panel.Opened -= OnOpened;
                panel.Closed -= OnClosed;
            }
        }

        private void OnOpened()
        {
            Time.timeScale = 0;
        }

        private void OnClosed()
        {
            Time.timeScale = 1;
        }
    }
}