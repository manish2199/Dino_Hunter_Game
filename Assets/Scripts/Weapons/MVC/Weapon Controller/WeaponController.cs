using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController 
{ 
    public WeaponView WeaponView { get; }

    public WeaponModel WeaponModel { get; }
	
   public WeaponController(WeaponModel weaponModel ,WeaponView weaponView)
   {
       WeaponModel = weaponModel;

          
       WeaponView = GameObject.Instantiate<WeaponView>(weaponView);
	//    WeaponView.WeaponGameObject.transform.SetParent(fpsTransform,false);
       WeaponView.WeaponGameObject.SetActive(false);

       WeaponView.WeaponController = this; 
	   Debug.Log("Weapon Unlocked");
   }

   public void ActivateWeapon(Transform fpsTransform)
   {
        // activate weapon gameobject
		// set position and rotation
	   WeaponView.WeaponGameObject.SetActive(true);
	   WeaponModel.IsWeaponActivated = true;
	   WeaponModel.PlayerFPS = fpsTransform;
	
	  WeaponView.WeaponGameObject.transform.SetParent(fpsTransform,false);
   }

   public void UseWeapon()
   {
        if(WeaponModel.IsWeaponActivated)
        {
          // 1 check if weapon is shootable or non shootable
           if(WeaponModel.WeaponType == WeaponType.Shootable)
           {
                WeaponAim();
           }
           WeaponAttack(); 
		}  
   }

   private void WeaponAim()
   {
       if(Input.GetMouseButtonDown(1))
       { 
        //  WeaponView.WeaponAnimator.SetBool(WeaponAnimatorParameters.AimBooleanText, true);
         WeaponView.WeaponAnimator.SetBool("AIM", true);
         Debug.Log(WeaponModel.WeaponType);

       }
       if(Input.GetMouseButtonUp(1))
       {
        //  WeaponView.WeaponAnimator.SetBool(WeaponAnimatorParameters.AimBooleanText, false);
         WeaponView.WeaponAnimator.SetBool("AIM", false);
       }
         
   }

   private void WeaponAttack()
   {
      if(Input.GetMouseButtonDown(0))
      {
         WeaponView.WeaponAnimator.SetTrigger(WeaponAnimatorParameters.ShootTriggerText);
         Debug.Log("Left Mouse Clicked");
         // check can shoot single bullet or multiple bullets
      }
   }


   public void DeactivateWeapon()
   {
      // deactivate gameobject
	  // remove the parent
       WeaponModel.IsWeaponActivated =  false;
	   WeaponView.WeaponGameObject.SetActive(false);
	//    WeaponView.WeaponTransform.SetParent(null);  
	   WeaponModel.PlayerFPS = null;
   }


}
