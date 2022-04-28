using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewPlayerScriptableObjectList")]
public class WaponScriptableObjectList : ScriptableObject
{
   public WeaponScriptableObject[] weaponsList;

   public WeaponScriptableObject GetWeapon(WeaponsID ID)
   {
      WeaponScriptableObject weapon = null;
      for( int i=0; i< weaponsList.Length; i++)
      {
         if(weaponsList[i].Weapon.weaponID == ID)
         {
            weapon = weaponsList[i];
            break;
         }
      }
      return weapon;
   }
}
