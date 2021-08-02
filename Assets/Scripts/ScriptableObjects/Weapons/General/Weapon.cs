using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "GenericWeapon", menuName = "Weapon/Generic", order = 1)]
public class Weapon : ScriptableObject
{
    public string weaponName = "test item";
    public string weaponDescription = "test item description";
    public Sprite sprite;
    public float critChance;
    public float critMultiplier;
    public List<WeaponAttack> attacks;
}

[System.Serializable]
public class WeaponAttack
{
    public float damage;
    public Vector2 hitboxOffset;
    public Vector2 hitboxSize;
    public Vector2 knockbackAngle;
    public float knockbackSpeed;
    public AnimationClip animation;
    public string animationClipName;
}
