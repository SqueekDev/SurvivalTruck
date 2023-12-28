using TMPro;
using UnityEngine;
using Lean.Localization;

public class UpgradeButtonView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _coinsPriceText;
    [SerializeField] private LeanPhrase _descriprionPhrase;
    [SerializeField] private LeanPhrase _pricePhrase;
    [SerializeField] private UpgradeButton _stats;

    private string _startDescription;

    public virtual void ChangeValues(int upgradeValue, int currentValue, int price)
    {
        _startDescription = LeanLocalization.GetTranslationText(_descriprionPhrase.name);
        _descriptionText.text = "+" + upgradeValue.ToString() + " " + _startDescription + currentValue.ToString();

        if (currentValue < _stats.MaxValue)
        {
            _coinsPriceText.text = price.ToString();
        }
        else
        {
            _coinsPriceText.text = LeanLocalization.GetTranslationText(_pricePhrase.name);
            _coinsPriceText.color = Color.red;
        }
    }
}
