using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
   private PlayerScriptableObject playerScriptableObject;  
    
   void Start()
   {
       playerScriptableObject  = Player.Instance.PlayerScriptableObject;
       SelectInitialWeapon(Player.Instance.weaponsHolder);  
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


    private void SelectInitialWeapon(Transform fps)
    {
        WeaponService.Instance.SelectInitialWeapon(fps); 
    }

    private void SelectWeapons(Transform fpsTransform , int weaponIndex)
    {
        WeaponService.Instance.SelectWeapon(fpsTransform,weaponIndex);
    }
}
