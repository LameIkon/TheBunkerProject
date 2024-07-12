using System.Collections;
using UnityEngine;

public abstract class WeaponBase : ScriptableObject
{
    [Header("Weapon")]
    public string _Name;
    [TextArea(2, 3)]
    public string _Discription;
    public Sprite _WeaponSprite;

    [Space(3f),Header("Damage")]
    public int _MinDamage;
    public int _MaxDamage;
    public float _CritChance;
    public float _CritMultiplyer;

    /// <summary>
    /// The Attack method is a Coroutine, this makes it possible to attach the attack to the animation. The Coroutine cannot run on a ScriptableObject but must run on a MonoBehaviour, which is the reason it takes this as a parameter.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public abstract IEnumerator Attack(AttackTestScript player);

    public virtual void Draw(AttackTestScript player) { }
    protected int DamageOutput()
    {
        float damage = Random.Range(_MinDamage, _MaxDamage+1);

        if (Random.Range(0, 100) <= _CritChance) 
        {
            damage *= _CritMultiplyer;
        }

        return (int)Mathf.Floor(damage); 
    }

}
