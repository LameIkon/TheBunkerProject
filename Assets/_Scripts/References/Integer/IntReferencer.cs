using UnityEngine;

[CreateAssetMenu(menuName = "Referencer/Int")]
public class IntReferencer : ScriptableObject
{

    [SerializeField] private IntVariable _maniputatedValue;
    [SerializeField] private IntReference _maxValue;
    [SerializeField] private IntReference _minValue;


    public void ApplyChange(int newValue) 
    {
        _maniputatedValue.ApplyChange(newValue);
        HandleValueManip();
    }

    public void SetValue(int newValue) 
    {
        _maniputatedValue.SetValue(newValue);
        HandleValueManip();
    }

    private void HandleValueManip() 
    {

        if (_maniputatedValue.GetValue() > _maxValue) 
        {
            _maniputatedValue.SetValue(_maxValue);
        }

        if (_maniputatedValue.GetValue() < _minValue) 
        {
            _maniputatedValue.SetValue(_minValue);
        }
    
    }

    public int GetMaxValue() 
    {
        return _maxValue;
    }

    public int GetMinValue() 
    {
        return _minValue; 
    }

    public int GetValue() 
    {
        return _maniputatedValue.GetValue();
    }

}

