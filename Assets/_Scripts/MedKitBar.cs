using UnityEngine;
using UnityEngine.UI;

sealed class MedKitBar : MonoBehaviour
{
    public Slider _Slider;
    public Gradient _Gradient;
    public Image _Fill;

    public void SetMedKitCount(uint count)
    {
        _Slider.value = count;
        _Fill.color = _Gradient.Evaluate(_Slider.normalizedValue);
    }
}
