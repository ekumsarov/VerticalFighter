using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VecrticalFighter.Model;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _coinText;

    public void Init(Level level, PlayerShip ship, PlayerFuel fuel)
    {
        ship.OnHpChanged += OnHpChanged;

        _slider.minValue = 0;
        _slider.wholeNumbers = true;
        _slider.maxValue = fuel.MaxFuel;
        _slider.value = _slider.maxValue;

        fuel.OnChanged += OnFuelChanged;
        _coinText.text = "0";
    }

    public void OnHpChanged(int currentHP)
    {
        _healthText.text = currentHP.ToString();
    }

    public void OnFuelChanged(int current)
    {
        _slider.value = current;
    }

    public void OnCoinChanged(int currentCoins)
    {
        _coinText.text = currentCoins.ToString();
    }
}
