using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class WeaponView : MonoBehaviour
{
  [SerializeReference] public WeaponController WeaponController;

   public Transform WeaponTransform;

   public GameObject WeaponGameObject;

   public Animator WeaponAnimator;

   public ParticleSystem MuzzleFire;
  
   public ParticleSystem HitEffect; 

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


   public void FireBullet()
   {
      ShootableWeaponController temp =(ShootableWeaponController)WeaponController;
      temp.FireBullet();
    }

    public void PlayAttackClip()
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
      ShootableWeaponController temp =(ShootableWeaponController)WeaponController;
      temp.PlayReloadClip1();
    }

    public void PlayReloadClip2()
    {
      ShootableWeaponController temp =(ShootableWeaponController)WeaponController;
      temp.PlayReloadClip2();
    }

}
