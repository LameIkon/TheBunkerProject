using TMPro;
using UnityEngine;
using UnityEngine.UI;

sealed class HealthBar : MonoBehaviour
{
    public Slider _Slider;
    public Gradient _Gradient;
    public Image _Fill;
    public TextMeshProUGUI _HealthCount;

    public void SetMaxHealth(int health)
    {
        _Slider.maxValue = health;
        _Slider.value = health;
        _Fill.color = _Gradient.Evaluate(1f);
        _HealthCount.text = health.ToString();
    }

    public void SetHealth(int health)
    {
        _Slider.value = health;
        _Fill.color = _Gradient.Evaluate(_Slider.normalizedValue);
        _HealthCount.text = health.ToString();
    }
}