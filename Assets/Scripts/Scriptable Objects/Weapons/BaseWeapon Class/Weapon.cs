using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Weapon 
{
    // public WeaponControls weaponControls;    

    public WeaponsID weaponID;     

    public WeaponView weaponView; 
}

[System.Serializable]
public class NonShootableWeapons : Weapon
{
   public int Damage;
}


[System.Serializable]
public class ShootableWeapons : Weapon
{    
    public ProjectileType projectileType;

    public FireType fireType;

    public float fireRate ;

    public float ShootingRange; 
    // private float nextTimeToFire;  ( Inside Weapon Controller )

    // Projectile ScriptableObject
}


// [System.Serializable]
// public class WeaponControls
// {
//     public KeyCode KeyToSelect;
//     public KeyCode KeyToAttack;
//     public KeyCode KeyToAim;
// }



public enum WeaponType
{
    None,
    Shootable,
    NonShootable
}

public enum WeaponsID
{
    None,
    Axe,
    Bow,
    SpearThrower,
    Revolver,
    ShotGun,
    AssaultRifle
}


public enum ProjectileType
{
   None,
   Arrow,
   Spear,
   Bullet
}

public enum FireType
{
    None,
    Single,
    Multiple
}