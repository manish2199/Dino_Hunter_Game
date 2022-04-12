using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesService : GenericSingleton<EnemiesService>
{
   // initially just use for testing 
   // then implement using object pooling 

   public WalkableDinosaurScriptableObject walkableDinosaurScriptableObject;

   public Transform positionToInstatiate;

   public Transform[] waypoints; 

   void Start()
   {
      RaptorDinosaurModel blueRaptorModel = new RaptorDinosaurModel(walkableDinosaurScriptableObject);

      Raptor raptor =(Raptor)walkableDinosaurScriptableObject.WalkingDinosaurs;

      RaptorDinosaurView view =(RaptorDinosaurView)raptor.WalkableDinosaurView;

      RaptorDinosaurController controller = new RaptorDinosaurController(blueRaptorModel,view,positionToInstatiate,waypoints);
   }
   
}
