using Agava.YandexGames;
using Base;
using Lean.Localization;
using UI;
using UnityEngine;

namespace YandexSDK
{
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

        private void ChangeLanguage()
        {
            if (UnityEngine.PlayerPrefs.HasKey(PlayerPrefsKeys.Language))
            {
                int languageNumber = UnityEngine.PlayerPrefs.GetInt(PlayerPrefsKeys.Language);

                switch (languageNumber)
                {
                    case EnglishNumber:
                        LeanLocalization.SetCurrentLanguageAll(English);
                        break;
                    case RussianNumber:
                        LeanLocalization.SetCurrentLanguageAll(Russian);
                        break;
                    case TurkishNumber:
                        LeanLocalization.SetCurrentLanguageAll(Turkish);
                        break;
                    default:
                        LeanLocalization.SetCurrentLanguageAll(English);
                        break;
                }
            }
            else
            {
#if UNITY_WEBGL && !UNITY_EDITOR
            _systemLanguage = YandexGamesSdk.Environment.i18n.lang;
#endif
                switch (_systemLanguage)
                {
                    case EnglishCode:
                        SetLanguage(English, EnglishNumber);
                        break;
                    case RussianCode:
                        SetLanguage(Russian, RussianNumber);
                        break;
                    case TurkishCode:
                        SetLanguage(Turkish, TurkishNumber);
                        break;
                    default:
                        SetLanguage(English, EnglishNumber);
                        break;
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

        private void OnLanguageChanged(int currentLanguageNumber)
        {
            switch (currentLanguageNumber)
            {
                case EnglishNumber:
                    SetLanguage(English, currentLanguageNumber);
                    break;
                case RussianNumber:
                    SetLanguage(Russian, currentLanguageNumber);
                    break;
                case TurkishNumber:
                    SetLanguage(Turkish, currentLanguageNumber);
                    break;
            }

            LeanLocalization.UpdateTranslations();
        }
    }
}