using Level;
using TMPro;
using UnityEngine;

namespace UI
{
    public class NextLevelButton : GameButton
    {
        [SerializeField] private LevelChanger _levelChanger;
        [SerializeField] private TMP_Text _nextLevelText;
        [SerializeField] private TMP_Text _bossLevelText;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (_levelChanger.CurrentLevelNumber % _levelChanger.BossLevelNumber == 0)
            {
                _nextLevelText.gameObject.SetActive(false);
                _bossLevelText.gameObject.SetActive(true);
            }
            else
            {
                _nextLevelText.gameObject.SetActive(true);
                _bossLevelText.gameObject.SetActive(false);
            }
        }
    }
}