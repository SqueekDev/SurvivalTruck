using TMPro;
using UnityEngine;
using Lean.Localization;

public class UpgradeButtonView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _coinsPriceText;
    [SerializeField] private LeanPhrase _phrase;

    private string _startDescription;

    public virtual void ChangeValues(int upgradeValue, int currentValue, int price)
    {
        _startDescription = LeanLocalization.GetTranslationText(_phrase.name);
        _descriptionText.text = "+" + upgradeValue.ToString() + " " + _startDescription + currentValue.ToString();
        _coinsPriceText.text = price.ToString();
    }
}
