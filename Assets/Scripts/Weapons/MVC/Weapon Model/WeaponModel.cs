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
