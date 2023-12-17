using UnityEngine;
using Lean.Localization;
using Agava.YandexGames;

public class Localization : MonoBehaviour
{
    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";
    private const string English = "English";
    private const string Russian = "Russian";
    private const string Turkish = "Turkish";
    private const int EnglishNumber = 0;
    private const int RussianNumber = 1;
    private const int TurkishNumber = 2;

    [SerializeField] private LanguageButton _languageButton;

    private string _systemLanguage;

    private void OnEnable()
    {
        _languageButton.LanguageChanged += OnLanguageChanged;
    }

    private void Start()
    {
        ChangeLanguage();
    }

    private void OnDisable()
    {
        _languageButton.LanguageChanged -= OnLanguageChanged;
    }

    private void OnLanguageChanged(int currentLanguageNumber)
    {
        if (currentLanguageNumber == EnglishNumber)
            SetLanguage(English, currentLanguageNumber);
        else if (currentLanguageNumber == RussianNumber)
            SetLanguage(Russian, currentLanguageNumber);
        else if (currentLanguageNumber == TurkishNumber)
            SetLanguage(Turkish, currentLanguageNumber);

        LeanLocalization.UpdateTranslations();
    }

    private void ChangeLanguage()
    {
        if (UnityEngine.PlayerPrefs.HasKey(PlayerPrefsKeys.Language))
        {
            int languageNumber = UnityEngine.PlayerPrefs.GetInt(PlayerPrefsKeys.Language);

            if (languageNumber == EnglishNumber)
                LeanLocalization.SetCurrentLanguageAll(English);
            else if (languageNumber == RussianNumber)
                LeanLocalization.SetCurrentLanguageAll(Russian);
            else if (languageNumber == TurkishNumber)
                LeanLocalization.SetCurrentLanguageAll(Turkish);
            else
                LeanLocalization.SetCurrentLanguageAll(English);
        }
        else
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _systemLanguage = YandexGamesSdk.Environment.i18n.lang;
#endif
            if (_systemLanguage != null)
            {
                if (_systemLanguage == EnglishCode)
                    SetLanguage(English, EnglishNumber);
                else if (_systemLanguage == RussianCode)
                    SetLanguage(Russian, RussianNumber);
                else if (_systemLanguage == TurkishCode)
                    SetLanguage(Turkish, TurkishNumber);
                else
                    SetLanguage(English, EnglishNumber);
            }
            else
            {
                SetLanguage(English, EnglishNumber);
            }
        }

        LeanLocalization.UpdateTranslations();
    }

    private void SetLanguage(string language, int number)
    {
        LeanLocalization.SetCurrentLanguageAll(language);
        SetPlayerPrefsNumber(number);
    }

    private void SetPlayerPrefsNumber(int number)
    {
        UnityEngine.PlayerPrefs.SetInt(PlayerPrefsKeys.Language, number);
        UnityEngine.PlayerPrefs.Save();
    }
}
