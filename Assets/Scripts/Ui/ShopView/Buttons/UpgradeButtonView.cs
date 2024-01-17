using Lean.Localization;
using Shop;
using TMPro;
using UnityEngine;

namespace UI
{
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
            string description = $"+{upgradeValue} {_startDescription}{currentValue}";
            _descriptionText.text = description;

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
}