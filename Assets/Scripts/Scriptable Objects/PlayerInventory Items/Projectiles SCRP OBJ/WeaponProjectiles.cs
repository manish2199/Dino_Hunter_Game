using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewWeaponProjectileScriptableObject")]
public class WeaponProjectiles : InventoryItem
{
    [SerializeField] private ProjectileType ProjectileType;
    
    public ProjectileType BulletType { get { return ProjectileType; } }

}

