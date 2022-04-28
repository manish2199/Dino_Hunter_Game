using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewWalkableDinosaurScriptableObject")]
public class WalkableDinosaurScriptableObject : ScriptableObject
{  
   [SerializeField] WalkingDinosaurType walkingDinosaurType ;

   [SerializeReference] WalkingDinosaurs DinosaurConstraints;
   public WalkingDinosaurs WalkingDinosaurs { get { return DinosaurConstraints; }}

   WalkingDinosaurType lastWalkableType ;
   
   private bool IsUnityLoaded = false; 
 

   public WalkingDinosaurType WalkingDinosaurType => walkingDinosaurType;  
   
   public void Awake()
   {
        lastWalkableType = WalkingDinosaurType;  
        IsUnityLoaded = true;
   }


    public WalkableDinosaurScriptableObject()
    {
        lastWalkableType = WalkingDinosaurType;
    }


   void OnValidate()
   {
       if(lastWalkableType != walkingDinosaurType  && IsUnityLoaded)
       {
          lastWalkableType = walkingDinosaurType;
          DinosaurConstraints = UpdateWalkingDinosaur(walkingDinosaurType); 
       }
   }
  
   WalkingDinosaurs UpdateWalkingDinosaur(WalkingDinosaurType walkingDinosaurType)
   {
       WalkingDinosaurs newWalkingDinosaurs = null;
        
      if(walkingDinosaurType == WalkingDinosaurType.Raptors)
      {
         newWalkingDinosaurs = new Raptor();
      }
      if(walkingDinosaurType == WalkingDinosaurType.TRex)
      {
         newWalkingDinosaurs = new TRex();
      }
      return newWalkingDinosaurs;
   }
}
