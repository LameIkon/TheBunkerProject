using UnityEngine;
using UnityEngine.UI;

sealed class HealthBar : MonoBehaviour
{
    public Slider _Slider;
    public Gradient _Gradient;
    public Image _Fill;

    public void SetMaxHealth(int health)
    {
        _Slider.maxValue = health;
        _Slider.value = health;
        _Fill.color = _Gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        _Slider.value = health;
        _Fill.color = _Gradient.Evaluate(_Slider.normalizedValue);
    }
}