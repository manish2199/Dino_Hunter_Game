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
      TRexDinosaurModel redTRexModel = new TRexDinosaurModel(walkableDinosaurScriptableObject);

      TRex trex =(TRex)walkableDinosaurScriptableObject.WalkingDinosaurs;

      TRexView view =(TRexView)trex.WalkableDinosaurView;

      TRexDinosaurController controller = new TRexDinosaurController(redTRexModel,view,positionToInstatiate,waypoints);
   }
   
}
