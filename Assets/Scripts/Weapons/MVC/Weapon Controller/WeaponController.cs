using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController 
{ 
   public WeaponView WeaponView { get; protected set; }

   public WeaponModel WeaponModel { get; protected set; }
	
   public WeaponController(){}

   public WeaponController(WeaponModel weaponModel ,WeaponView weaponView)
   {
       WeaponModel = weaponModel;
          
       WeaponView = GameObject.Instantiate<WeaponView>(weaponView);

       WeaponView.WeaponGameObject.SetActive(false);

       WeaponView.WeaponController = this; 
   }

   public virtual void ActivateWeapon(Transform fpsTransform)
   {
	   WeaponView.WeaponGameObject.SetActive(true);
	   WeaponModel.IsWeaponActivated = true;
	   WeaponModel.PlayerFPS = fpsTransform;
	
	   WeaponView.WeaponGameObject.transform.SetParent(fpsTransform,false);
   }

   public virtual void UseWeapon(){}
 
   protected virtual void WeaponAttack()
   {
      if(Input.GetMouseButtonDown(0))
      {
         WeaponView.WeaponAnimator.SetTrigger(WeaponAnimatorParameters.ShootTriggerText);
      }
   }


   public virtual void DeactivateWeapon()
   {
      WeaponModel.IsWeaponActivated = false;
	   WeaponView.WeaponGameObject.SetActive(false);  
	   WeaponModel.PlayerFPS = null;
   }


}



public class NonShootableWeaponController : WeaponController
{ 	
   public NonShootableWeaponController(WeaponModel weaponModel ,WeaponView weaponView) : base ( weaponModel , weaponView)
   {}

   public override void ActivateWeapon(Transform fpsTransform)
   {
	   base.ActivateWeapon(fpsTransform);
   }

   public override void UseWeapon()
   {
      WeaponAttack();   
   }

   protected override void WeaponAttack()
   {
     base.WeaponAttack();
   }

   public void DeactivateWeapon()
   {
      base.DeactivateWeapon();
   }

}


public class ShootableWeaponController : WeaponController
{ 	
   public ShootableWeaponModel ShootableWeaponModel { get; }

   public ShootableWeaponController(ShootableWeaponModel weaponModel ,WeaponView weaponView) 
   {
      this.ShootableWeaponModel = weaponModel;
          
      WeaponView = GameObject.Instantiate<WeaponView>(weaponView);

      WeaponView.WeaponGameObject.SetActive(false);

      WeaponView.WeaponController = this; 
   }

   public override void ActivateWeapon(Transform fpsTransform)
   {
	   // base.ActivateWeapon(fpsTransform); 
      WeaponView.WeaponGameObject.SetActive(true);
	   ShootableWeaponModel.IsWeaponActivated = true;
	   ShootableWeaponModel.PlayerFPS = fpsTransform;
	
	   WeaponView.WeaponGameObject.transform.SetParent(fpsTransform,false);

   }

   public override void UseWeapon()
   {
      if(ShootableWeaponModel.CanHaveAimAnimation)
      {
         WeaponAimAnimation();
      }
      else
      {
         ZoomInOut();
      }
      WeaponAttack();   
   }

   private void WeaponAimAnimation()
   {
       if(Input.GetMouseButtonDown(1))
       { 
        //  WeaponView.WeaponAnimator.SetBool(WeaponAnimatorParameters.AimBooleanText, true);
         WeaponView.WeaponAnimator.SetBool(WeaponAnimatorParameters.AimBooleanText ,true);
       }
       if(Input.GetMouseButtonUp(1))
       {
        //  WeaponView.WeaponAnimator.SetBool(WeaponAnimatorParameters.AimBooleanText, false);
         WeaponView.WeaponAnimator.SetBool(WeaponAnimatorParameters.AimBooleanText ,false);
       }
   }

   private void ZoomInOut()
   {
       if(Input.GetMouseButtonDown(1))
       { 
          // Zoom In
       }
       if(Input.GetMouseButtonUp(1))
       {
          // Zoom Out
       }
   }

   protected override void WeaponAttack()
   {
      // WeaponView.WeaponAnimator.SetTrigger(WeaponAnimatorParameters.ShootTriggerText);
      // check whether it has single shot round or multiple shot round

      if(ShootableWeaponModel.FireType == FireType.Multiple)
      {
            // means assualt rifle
            ShootBulletMultileTime();
      }
      else
      {
            // means other weapons which shot only once
            ShootBulletSingleTime();
      }
   } 

   private void ShootBulletSingleTime()
   {
      if(Input.GetMouseButtonDown(0))
      {
         WeaponView.WeaponAnimator.SetTrigger(WeaponAnimatorParameters.ShootTriggerText);
      }
   }


   private void ShootBulletMultileTime()
   {
      if(Input.GetMouseButtonDown(0) && Time.time > ShootableWeaponModel.NextTimeToShoot )
      {
         ShootableWeaponModel.NextTimeToShoot = Time.time + (1 / ShootableWeaponModel.FireRate );  
         WeaponView.WeaponAnimator.SetTrigger(WeaponAnimatorParameters.ShootTriggerText);   
      
        // Fire Bullet
      }
   }

   public override void DeactivateWeapon()
   {
      ShootableWeaponModel.IsWeaponActivated = false;
      WeaponView.WeaponGameObject.SetActive(false);  
	   ShootableWeaponModel.PlayerFPS = null;
   }

}
