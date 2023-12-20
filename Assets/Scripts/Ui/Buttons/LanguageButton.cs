using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageButton : GameButton
{
    private const int EnglishNumber = 0;
    private const int RussianNumber = 1;
    private const int TurkishNumber = 2;

    [SerializeField] private List<Sprite> _icons;

    public event Action<int> LanguageChanged;

    private void Start()
    {
        int languageNumber = PlayerPrefs.GetInt(PlayerPrefsKeys.Language, EnglishNumber);
        SetLanguageSprite(languageNumber);
    }

    private void SwitchLanguage()
    {
        int currentLanguageNumber = PlayerPrefs.GetInt(PlayerPrefsKeys.Language, EnglishNumber);
        currentLanguageNumber++;

        if (currentLanguageNumber > _icons.Count - GlobalValues.ListIndexCorrection)
        {
            currentLanguageNumber -= _icons.Count;
        }

        SetLanguageSprite(currentLanguageNumber);
        LanguageChanged?.Invoke(currentLanguageNumber);
    }

    private void SetLanguageSprite(int languageNumber)
    {
        if (languageNumber == EnglishNumber)
        {
            Button.image.sprite = _icons[EnglishNumber];
        }
        else if (languageNumber == RussianNumber)
        {
            Button.image.sprite = _icons[RussianNumber];
        }
        else if (languageNumber == TurkishNumber)
        {
            Button.image.sprite = _icons[TurkishNumber];
        }
    }

    protected override void OnButtonClick()
    {
        base.OnButtonClick();
        SwitchLanguage();
    }
}
