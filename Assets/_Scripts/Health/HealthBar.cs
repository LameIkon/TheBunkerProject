using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

sealed class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _fill;
    [SerializeField] private TextMeshProUGUI _healthCounter;
    [SerializeField] private FloatVariable _health;
    [SerializeField] private FloatVariable _maxHealth;

    private void Awake()
    {
        SetMaxHealth(_maxHealth);
    }

    private void Update()
    {
        SetHealth(_health);
    }

    private void SetMaxHealth(FloatVariable health)
    {
        _slider.maxValue = health;
        _slider.value = health;
        _fill.color = _gradient.Evaluate(1f);
        _healthCounter.text = health.GetValue().ToString();
    }

    private void SetHealth(FloatVariable health)
    {
        _slider.value = health;
        _healthCounter.text = health.GetValue().ToString();
        _fill.color = _gradient.Evaluate(_slider.normalizedValue);
        
        if (health >= _maxHealth)
        {
            health.SetValue(_maxHealth);    
        }
    }
}