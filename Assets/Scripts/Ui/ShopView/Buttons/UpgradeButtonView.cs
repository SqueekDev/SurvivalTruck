using TMPro;
using UnityEngine;

public class UpgradeButtonView : MonoBehaviour
{
    [SerializeField] private UpgradeButton _upgradeButton;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _coinsPriceText;

    private string _startDescription;

    private void Awake()
    {
        _startDescription = _descriptionText.text;
    }

    private void OnEnable()
    {
        _upgradeButton.ValuesChanged += OnValuesChanged;
    }

    private void OnDisable()
    {
        _upgradeButton.ValuesChanged -= OnValuesChanged;        
    }

    protected virtual void OnValuesChanged(int upgradeValue, int currentValue, int price)
    {
        _descriptionText.text = "+" + upgradeValue.ToString() + " " + _startDescription + currentValue.ToString();
        _coinsPriceText.text = price.ToString();
    }
}
