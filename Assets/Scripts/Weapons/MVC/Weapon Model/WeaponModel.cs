using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel 
{
   public WeaponModel(WeaponScriptableObject waponScriptableObject)
   {
      WeaponType = waponScriptableObject.WeaponType;  
   }
  
   public bool IsWeaponActivated { get; set; }

   public Transform PlayerFPS { get; set;}

   public WeaponType WeaponType { get; }  
}


public class ShootableWeaponModel : WeaponModel
{
   public ShootableWeaponModel(WeaponScriptableObject waponScriptableObject) : base ( waponScriptableObject )
   {
      ShootableWeapons temp =(ShootableWeapons)waponScriptableObject.Weapon;
     
      this.CanHaveAimAnimation = temp.CanHaveAimAnimation;
      this.FireType = temp.fireType;
      this.ProjectileType = temp.projectileType;
      this.FireRate = temp.fireRate;
      ShootingRange = temp.ShootingRange; 
   }
 
  public bool CanHaveAimAnimation { get; }

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
