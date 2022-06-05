using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class WeaponView : MonoBehaviour
{
   public WeaponController WeaponController;

   public Transform WeaponTransform;

   public GameObject WeaponGameObject;

   public Animator WeaponAnimator;

   public ParticleSystem MuzzleFire;
  
   public ParticleSystem MetalHitEffect; 
   public ParticleSystem BloodHitEffect;

   public LayerMask EnemyLayerMask;

   public GameObject muzzle;

   public Transform ZoomInMuzzlePosition;
   public Transform ZoomOutMuzzlePosition;


   public AudioSource WeaponAudioSource;
 
   private void Update()
   {
       WeaponController.UseWeapon();
   }
  
   public void PlayMuzzleFlash()
   {
      MuzzleFire.Emit(1);
   }

    private void FireBullet()
   {
      ShootableWeaponController temp =(ShootableWeaponController)WeaponController;
      temp.FireBullet();
    }

    private void PlayAttackClip()
    { 
      WeaponAudioSource.Stop();
      WeaponController.PlayAttackClip();
    }


    public void HandleShooting()
    {
      PlayMuzzleFlash();

      FireBullet();

      PlayAttackClip();
    }

    public void PlayReloadClip1()
    {
      var shootableController =(ShootableWeaponController)WeaponController;
      shootableController.PlayReloadClip1();
    }

    public void PlayReloadClip2()
    {
      var shootableController =(ShootableWeaponController)WeaponController;
      shootableController.PlayReloadClip2();
    }

}
