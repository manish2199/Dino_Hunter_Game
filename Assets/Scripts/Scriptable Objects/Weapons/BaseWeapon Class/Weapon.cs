using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Weapon 
{
    // public WeaponControls weaponControls;    

    public WeaponsID weaponID;     

    public WeaponView weaponView; 

    public int Damage;

    public AudioClip AttackClip;
}

[System.Serializable]
public class NonShootableWeapons : Weapon
{}


[System.Serializable]
public class ShootableWeapons : Weapon
{   
    public ProjectileType projectileType;

    public FireType fireType;

    public float fireRate ;

    public float ShootingRange; 

    public AudioClip[] ReloadClip;

    

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
    Revolver,
    ShotGun,
    AssaultRifle
}


public enum ProjectileType
{
   None,
   RevolverBullet,
   ShotGunBullet,
   AssaultRifleBullet
}

public enum FireType
{
    None,
    Single,
    Multiple
}

