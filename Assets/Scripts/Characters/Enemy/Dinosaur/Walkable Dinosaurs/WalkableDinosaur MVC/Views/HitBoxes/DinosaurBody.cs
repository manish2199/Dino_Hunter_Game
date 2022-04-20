using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurBody : MonoBehaviour , IDamagable
{
   [SerializeField] DinosaurBodyPartType dinosaurBodyPart;

   [SerializeReference] WalkableDinosaurView walkingDinosaurView;
  
   void Update(){}

   public static event Action<RaptorsType,WalkableDinosaurView> OnHeadshot;
   public static event Action OnBodyShot;


   public void TakeDamage(int damage)
   {
      if(dinosaurBodyPart == DinosaurBodyPartType.Head)
      {
         // recieve double damage 
        
         walkingDinosaurView.walkableDinosaurController.TakeDamage(damage*2);
        if(walkingDinosaurView.walkableDinosaurController.WalkableDinosaurModel.WalkingDinosaurType == WalkingDinosaurType.Raptors)
        {
        
          RaptorDinosaurModel temp = (RaptorDinosaurModel)walkingDinosaurView.walkableDinosaurController.WalkableDinosaurModel;
        
          OnHeadshot?.Invoke(temp.RaptorsType,walkingDinosaurView);
        
        }
      }
      else if( dinosaurBodyPart == DinosaurBodyPartType.MainBody)
      {
         // recieve less damage than head
         walkingDinosaurView.walkableDinosaurController.TakeDamage(damage);
         OnBodyShot?.Invoke();
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
