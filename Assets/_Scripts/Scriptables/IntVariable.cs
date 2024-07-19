using UnityEngine;

[CreateAssetMenu(fileName = "New Int Variable", menuName = "Variable/Int")]
public class IntVariable : VariableBase
{

    public int _Value;

    [SerializeField, Tooltip("Called when the value of the Variable is changed, it can also be null")]
    private GameEvent _onValueChanged;



    #region Setters and Getters    

    public void SetValue(int value)
    {
        _Value = value;
        OnValueChanged();
    }

    public void SetValue(IntVariable value)
    {
        _Value = value._Value;
        OnValueChanged();
    }

    public void ApplyChange(int amount)
    {
        _Value += amount;
        OnValueChanged();
    }

    public void ApplyChange(IntVariable amount)
    {
        _Value += amount._Value;
        OnValueChanged();
    }

    public int GetValue()
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

    public static implicit operator int(IntVariable v)
    {
        return v.GetValue();
    }

}