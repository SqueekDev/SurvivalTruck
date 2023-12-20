using UnityEngine;
using TMPro;

public class NextLevelButton : GameButton
{
    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private TMP_Text _nextLevelText;
    [SerializeField] private TMP_Text _bossLevelText;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (_levelChanger.CurrentLevelNumber % _levelChanger.BossLevelNumber == GlobalValues.Zero)
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
