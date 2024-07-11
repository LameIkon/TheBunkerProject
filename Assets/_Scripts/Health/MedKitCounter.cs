using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MedKitCounter : MonoBehaviour
{
    [SerializeField] private Image[] _MedKitIcons;
    [SerializeField] private TextMeshProUGUI _medKitCounter;
    [SerializeField] private float _OccupiedSlot = 1f;
    [SerializeField] private float _EmptySlot = 0.4f;
    private Color _alphaOccupied;
    private Color _alphaEmpty;

    private void Start()
    {
        _alphaOccupied = _MedKitIcons[0].color;
        _alphaOccupied.a = _OccupiedSlot;
        _alphaEmpty = _MedKitIcons[0].color;
        _alphaEmpty.a = _EmptySlot;
    }

    public void SetMedKitCountNew(uint numberOfMedKits)
    {
        _medKitCounter.text = numberOfMedKits.ToString();
        
        switch (numberOfMedKits)
        {
            case 3:
                for (int i = 0; i < _MedKitIcons.Length; i++)
                {
                    _MedKitIcons[i].color = _alphaOccupied;
                }
                break;

            case 2:
                for (int i = 0; i < _MedKitIcons.Length; i++)
                {
                    _MedKitIcons[0].color = _alphaOccupied;
                    _MedKitIcons[1].color = _alphaOccupied;
                    _MedKitIcons[2].color = _alphaEmpty;
                }
                break;

            case 1:
                for (int i = 0; i < _MedKitIcons.Length; i++)
                {
                    _MedKitIcons[0].color = _alphaOccupied;
                    _MedKitIcons[1].color = _alphaEmpty;
                    _MedKitIcons[2].color = _alphaEmpty;
                }
                break;

            case 0:
                for (int i = 0; i < _MedKitIcons.Length; i++)
                {
                    _MedKitIcons[i].color = _alphaEmpty;
                }
                break;
        }
    }
}