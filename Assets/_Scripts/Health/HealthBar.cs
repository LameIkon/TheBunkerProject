using TMPro;
using UnityEngine;
using UnityEngine.UI;

sealed class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _Slider;
    [SerializeField] private Gradient _Gradient;
    [SerializeField] private Image _Fill;
    [SerializeField] private TextMeshProUGUI _healthCounter;

    public void SetMaxHealth(int health)
    {
        _Slider.maxValue = health;
        _Slider.value = health;
        _Fill.color = _Gradient.Evaluate(1f);
        _healthCounter.text = health.ToString();
    }

    public void SetHealth(int health)
    {
        _Slider.value = health;
        _Fill.color = _Gradient.Evaluate(_Slider.normalizedValue);
        _healthCounter.text = health.ToString();
    }
}