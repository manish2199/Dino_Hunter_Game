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


   public virtual void PlayAttackClip()
   {
      WeaponView.WeaponAudioSource.clip = WeaponModel.AttackClip;
      WeaponView.WeaponAudioSource.Play();
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

   public override void DeactivateWeapon()
   {
      base.DeactivateWeapon();
   }

   public override void PlayAttackClip()
   {
      base.PlayAttackClip();
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
      WeaponView.WeaponGameObject.SetActive(true);
	   ShootableWeaponModel.IsWeaponActivated = true;
	   ShootableWeaponModel.PlayerFPS = fpsTransform;
	
	   WeaponView.WeaponGameObject.transform.SetParent(fpsTransform,false);
   }

   public override void UseWeapon()
   {
      ZoomInOut();

      WeaponAttack();   
   }

   

   private void ZoomInOut()
   {
       if(Input.GetMouseButtonDown(1))
       { 
          // Zoom In 
         //  WeaponService.Instance.WeaponZoomIn(true);
          WeaponService.Instance.InvokeOnZoomIn(true);
          WeaponView.muzzle.transform.position = WeaponView.ZoomInMuzzlePosition.position;
       }
       if(Input.GetMouseButtonUp(1))
       {
          // Zoom Out
          WeaponService.Instance.InvokeOnZoomIn(false);
          WeaponView.muzzle.transform.position = WeaponView.ZoomOutMuzzlePosition.position;
         //  WeaponService.Instance.WeaponZoomIn(false);
       }
   }

   protected override void WeaponAttack()
   {
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
         PlayShootingAnimation();
        // Fire Bullet 
         // FireBullet();
      }
   }


   private void ShootBulletMultileTime()
   {
      if(Input.GetMouseButton(0) && Time.time > ShootableWeaponModel.NextTimeToShoot )
      {
         ShootableWeaponModel.NextTimeToShoot = Time.time + (1 / ShootableWeaponModel.FireRate );  
         PlayShootingAnimation();         
         WeaponView.WeaponAudioSource.Stop();
         PlayAttackClip();
      }
      if(Input.GetMouseButtonUp(0))
      {
         WeaponView.WeaponAudioSource.Stop();
      }
   }

   private void PlayShootingAnimation()
   {
      WeaponView.WeaponAnimator.SetTrigger(WeaponAnimatorParameters.ShootTriggerText);   
   }

   public void PlayReloadClip1()
   {
      WeaponView.WeaponAudioSource.clip = ShootableWeaponModel.ReloadClip[0];
      WeaponView.WeaponAudioSource.Play();
   }

   public void PlayReloadClip2()
   {
      WeaponView.WeaponAudioSource.clip = ShootableWeaponModel.ReloadClip[0];
      WeaponView.WeaponAudioSource.Play();
   }

   public override void PlayAttackClip()
   {
      WeaponView.WeaponAudioSource.clip = ShootableWeaponModel.ProjectileFireClip;
      WeaponView.WeaponAudioSource.Play();
   }


   public void FireBullet()
   {
      // first check whether inventory has the sufficient bullet no or not if yes then fire
      

      RaycastHit hit;
       
      if(Physics.Raycast(ShootableWeaponModel.PlayerFPS.position,ShootableWeaponModel.PlayerFPS.forward,out hit))
      {
         // Debug.Log( " Hits " + hit.transform.gameObject.name);
         
         // Debug.DrawLine(ShootableWeaponModel.PlayerFPS.position,hit.point, Color.red , 1.0f ); 
         
         WeaponView.HitEffect.transform.position = hit.point;
         WeaponView.HitEffect.transform.forward = hit.normal;
         WeaponView.HitEffect.Emit(1);
      }
      
   }

   public override void DeactivateWeapon()
   {
      ShootableWeaponModel.IsWeaponActivated = false;
      WeaponView.WeaponGameObject.SetActive(false);  
	   ShootableWeaponModel.PlayerFPS = null;
	   WeaponView.WeaponGameObject.transform.SetParent(null,false);

   }

}

