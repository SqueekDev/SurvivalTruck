using UnityEngine;
using Lean.Localization;
using TMPro;
using Shop;

namespace UI
{
    public class AddCoinsOffer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _description;
        [SerializeField] private CoinCounter _coinCounter;
        [SerializeField] private LeanPhrase _descriptionFirstPart;
        [SerializeField] private LeanPhrase _descriptionSecondPart;

        private void OnEnable()
        {
            string offer = $"{LeanLocalization.GetTranslationText(_descriptionFirstPart.name)}" +
                $" {_coinCounter.CurrentAdReward}" +
                $" {LeanLocalization.GetTranslationText(_descriptionSecondPart.name)}";
            _description.text = offer;
        }
    }
}