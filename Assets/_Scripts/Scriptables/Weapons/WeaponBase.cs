using System.Collections;
using UnityEngine;

public abstract class WeaponBase : ScriptableObject
{
    public string _Name;
    [TextArea(2, 3)]
    public string _Discription;
    public Sprite _WeaponSprite;
    public float _Damage;

    public abstract IEnumerator Attack(MonoBehaviour player);

}
