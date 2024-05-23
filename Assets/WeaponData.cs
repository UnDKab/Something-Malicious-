using UnityEngine;

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Unique,
    Legendary
}

public enum DamageType
{
    Normal,
    Holy,
    Cursed,
    Fire,
    Poison,
    Ice
}

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public float baseDamage;
    public Rarity rarity;
    public DamageType damageType;
}