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
 
   private void Update()
   {
       WeaponController.UseWeapon();
   }
}
