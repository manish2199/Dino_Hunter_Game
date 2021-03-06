using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponModel 
{
   public WeaponModel(WeaponScriptableObject waponScriptableObject)
   {
      WeaponType = waponScriptableObject.WeaponType;  
      AttackClip = waponScriptableObject.Weapon.AttackClip;
      Damage = waponScriptableObject.Weapon.Damage;
      WeaponIcon = waponScriptableObject.Weapon.WeaponIcon;
   }
   
   public Sprite WeaponIcon { get; }
 
   public int Damage { get; }

   public AudioClip AttackClip { get;}
  
   public bool IsWeaponActivated { get; set; }

   public Transform PlayerFPS { get; set;}

   public WeaponType WeaponType { get; }  
}


public class ShootableWeaponModel : WeaponModel
{
   public ShootableWeaponModel(WeaponScriptableObject waponScriptableObject) : base ( waponScriptableObject )
   {
      ShootableWeapons temp =(ShootableWeapons)waponScriptableObject.Weapon;
     
      this.FireType = temp.fireType;
      this.ProjectileType = temp.projectileType;
      this.FireRate = temp.fireRate;
      ShootingRange = temp.ShootingRange; 
      ReloadClip = temp.ReloadClip;
      ProjectileFireClip = temp.AttackClip;
   }

  public AudioClip ProjectileFireClip { get;}

  public AudioClip[] ReloadClip { get;}
  
  public FireType FireType { get; }

  public ProjectileType ProjectileType;

  public float FireRate;

  public float ShootingRange; 

  public float NextTimeToShoot { get; set; }
}




public class NonShootableWeaponModel : WeaponModel
{
   public NonShootableWeaponModel(WeaponScriptableObject waponScriptableObject) : base ( waponScriptableObject )
   {}
}
