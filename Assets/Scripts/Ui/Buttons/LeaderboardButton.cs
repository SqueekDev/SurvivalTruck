using System;
using Agava.YandexGames;
using UnityEngine;

namespace UI
{
    public class LeaderboardButton : GameButton
    {
        [SerializeField] private GamePanel _loginPanel;

        public event Action AutorizationCompleted;

        protected override void OnButtonClick()
        {
            base.OnButtonClick();

            if (PlayerAccount.IsAuthorized == false)
            {
                _loginPanel.gameObject.SetActive(true);
            }
            else
            {
                AutorizationCompleted?.Invoke();
            }
        }
    }
}