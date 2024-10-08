using UnityEngine;

[CreateAssetMenu(fileName = "New Float Variable", menuName = "Variable/Float")]
public class FloatVariable : VariableBase
{

    public float _Value;

    [SerializeField, Tooltip("Called when the value of the Variable is changed, it can also be null")]
    private GameEvent _onValueChanged;



    #region Setters and Getters    

    public void SetValue(float value)
    {
        _Value = value;
        OnValueChanged();
    }

    public void SetValue(FloatVariable value)
    {
        _Value = value._Value;
        OnValueChanged();
    }

    public void ApplyChange(float amount)
    {
        _Value += amount;
        OnValueChanged();
    }

    public void ApplyChange(FloatVariable amount)
    {
        _Value += amount._Value;
        OnValueChanged();
    }

    public float GetValue()
    {
        return _Value;
    }

    #endregion

    // Used to update other scripts when they the value is changed
    private void OnValueChanged()
    {
        if (_onValueChanged != null)
        {
            _onValueChanged.Raise();
        }
    }

    public static implicit operator float(FloatVariable v)
    {
        return v.GetValue();
    }

}