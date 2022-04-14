using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurBody : MonoBehaviour , IDamagable
{
   [SerializeField] DinosaurBodyPartType dinosaurBodyPart;

   [SerializeReference] WalkableDinosaurView walkingDinosaurView;
  
   void Update(){}



   public void TakeDamage(int damage)
   {
      print("Damage Recieved");

      // Jobs

      // 1 Tell controller through view that recieved damage 




      if(dinosaurBodyPart == DinosaurBodyPartType.Head)
      {
          // recieve double damage 
      }
      else if( dinosaurBodyPart == DinosaurBodyPartType.MainBody)
      {
         // recieve less damage than head
      }
   }



   void OnTriggerEnter(Collider other)
   {
      if(dinosaurBodyPart == DinosaurBodyPartType.Head)
      {    
          IDamagable damagable = other.GetComponent<IDamagable>();

          if(damagable != null)
          {   
               print("Eating Player");
               damagable.TakeDamage(walkingDinosaurView.walkableDinosaurController.WalkableDinosaurModel.Damage);
          }
      }
   }


}
