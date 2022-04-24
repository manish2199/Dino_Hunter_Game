using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
   private PlayerScriptableObject playerScriptableObject;  
    
   void Start()
   {
       playerScriptableObject  = Player.Instance.PlayerScriptableObject;
   }


    public void SelectWeapon()
   { 
      if(Input.GetKeyDown(playerScriptableObject.playerControls.KeyToSelectWeapon1))
      {
         SelectWeapons(Player.Instance.weaponsHolder,0);
      }
      if(Input.GetKeyDown(playerScriptableObject.playerControls.KeyToSelectWeapon2))
      {
         SelectWeapons(Player.Instance.weaponsHolder,1);
      }
      if(Input.GetKeyDown(playerScriptableObject.playerControls.KeyToSelectWeapon3))
      {
         SelectWeapons(Player.Instance.weaponsHolder,2);
      }
      if(Input.GetKeyDown(playerScriptableObject.playerControls.KeyToSelectWeapon4))
      {
        SelectWeapons(Player.Instance.weaponsHolder,3);
      }
      
   }


   public void SelectInitialWeapon()
   {
      WeaponService.Instance.SelectInitialWeapon(Player.Instance.weaponsHolder); 
   }

   private void SelectWeapons(Transform fpsTransform , int weaponIndex)
   {
      WeaponService.Instance.SelectWeapon(fpsTransform,weaponIndex);
   }
}
