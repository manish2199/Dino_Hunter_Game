using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
   PlayerScriptableObject playerScriptableObject;
    
   Transform weaponsHolder;

   public void SelectWeapon()
   { 
      if(Input.GetKeyDown(playerScriptableObject.playerControls.KeyToSelectWeapon1))
      {
         SelectWeapons(weaponsHolder,0);
      }
      if(Input.GetKeyDown(playerScriptableObject.playerControls.KeyToSelectWeapon2))
      {
         SelectWeapons(weaponsHolder,1);
      }
      if(Input.GetKeyDown(playerScriptableObject.playerControls.KeyToSelectWeapon3))
      {
         SelectWeapons(weaponsHolder,2);
      }
      
   }

   public void InitialWeaponSetup()
   {
      playerScriptableObject = Player.Instance.PlayerScriptableObject;
      weaponsHolder = Player.Instance.weaponsHolder;
      WeaponService.Instance.SelectInitialWeapon(Player.Instance.weaponsHolder); 
   }

   private void SelectWeapons(Transform fpsTransform , int weaponIndex)
   {
      WeaponService.Instance.SelectWeapon(fpsTransform,weaponIndex);
   }
}
