using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponView : MonoBehaviour
{
   public WeaponController WeaponController;

   public Transform WeaponTransform;

   public GameObject WeaponGameObject;

   public Animator WeaponAnimator;
 
   private void Update()
   {
       WeaponController.UseWeapon();
   }
}
