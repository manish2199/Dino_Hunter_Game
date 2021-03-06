using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WalkableDinosaurModel 
{
   public WalkableDinosaurModel(WalkableDinosaurScriptableObject walkableDinosaurScriptableObject)
   {
      SetHealth(walkableDinosaurScriptableObject);
      Damage = walkableDinosaurScriptableObject.WalkingDinosaurs.Damage;
      Speed = walkableDinosaurScriptableObject.WalkingDinosaurs.Speed;
      AngularSpeed = walkableDinosaurScriptableObject.WalkingDinosaurs.AngularSpeed;
      Acceleration  = walkableDinosaurScriptableObject.WalkingDinosaurs.Acceleration;
      TargetStopppingDistance = walkableDinosaurScriptableObject.WalkingDinosaurs.TargetStopppingDistance;
      StoppingDistanceFromWayPoint = walkableDinosaurScriptableObject.WalkingDinosaurs.StoppingDistanceFromWayPoint;
      DinosaurAudios = walkableDinosaurScriptableObject.WalkingDinosaurs.DinosaurAudios; 
      ChasingRange = walkableDinosaurScriptableObject.WalkingDinosaurs.ChasingRange;
      AttackingRange = walkableDinosaurScriptableObject.WalkingDinosaurs.AttackingRange;
      FieldOfViewAnle = walkableDinosaurScriptableObject.WalkingDinosaurs.FieldOfViewAnle;
      WalkingDinosaurType = walkableDinosaurScriptableObject.WalkingDinosaurType;
      TimeToRespawnAfterDeath = walkableDinosaurScriptableObject.WalkingDinosaurs.TimeToRespawnAfterDeath;
      IsEnemyAlredyDetected = false;
   } 

   private void SetHealth(WalkableDinosaurScriptableObject walkableDinosaurScriptableObject)
   {
      if(GameData.GetEasyDifficulty() == 1)
      {
         Health = walkableDinosaurScriptableObject.WalkingDinosaurs.EasyDifficultyHealth;
      }
       if(GameData.GetMediumDifficulty() == 1)
      {
         Health = walkableDinosaurScriptableObject.WalkingDinosaurs.MediumDifficultyHealth;
      }
       if(GameData.GetHardDifficulty() == 1)
      {
         Health = walkableDinosaurScriptableObject.WalkingDinosaurs.HardDifficultyHealth;
      }

   }
  
   public bool IsEnemyAlredyDetected { get; set; }

   public int TimeToRespawnAfterDeath { get; }

   public WalkingDinosaurType WalkingDinosaurType {get;}

   public float FieldOfViewAnle { get; }

   public float ChasingRange { get; }

   public float AttackingRange { get; }

   public float StoppingDistanceFromWayPoint { get;}

   public DinosaurAudioClips[] DinosaurAudios;

   public int Health { get; protected set; }  

   public int HealhToReduce { get; set;}

   public int Damage { get; }

   public float Speed { get; }

   public float AngularSpeed { get; }
   
   public float Acceleration  { get; } 
 
   public float TargetStopppingDistance  { get; }

   public Transform[] WayPoints { get; set; } 
}    



public class RaptorDinosaurModel : WalkableDinosaurModel
{
  
  public RaptorDinosaurModel(WalkableDinosaurScriptableObject walkableDinosaurScriptableObject) : base ( walkableDinosaurScriptableObject) 
  {
      Raptor temp = (Raptor) walkableDinosaurScriptableObject.WalkingDinosaurs;

      RaptorsType = temp.raptorsType;
      SpecialAbility = temp.specialAbilities;
  }

   public RaptorsType RaptorsType { get; }

   public SpecialAbility SpecialAbility { get; }
    
}


public class TRexDinosaurModel : WalkableDinosaurModel
{
   public TRexDinosaurModel(WalkableDinosaurScriptableObject walkableDinosaurScriptableObject) : base ( walkableDinosaurScriptableObject) 
   {
      TRex temp = (TRex)walkableDinosaurScriptableObject.WalkingDinosaurs;

       TRexType = temp.TRexType;
    }

   public TRexType TRexType { get; }

}
