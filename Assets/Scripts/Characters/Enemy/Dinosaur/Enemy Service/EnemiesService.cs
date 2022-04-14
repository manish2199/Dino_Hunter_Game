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

   protected override void Awake()
   {
      if(Instance == null)
      {
         Instance = this;
      }
   }

   void Start()
   {
      RaptorDinosaurModel redTRexModel = new RaptorDinosaurModel(walkableDinosaurScriptableObject);

      Raptor trex =(Raptor)walkableDinosaurScriptableObject.WalkingDinosaurs;

      RaptorDinosaurView view =(RaptorDinosaurView)trex.WalkableDinosaurView;

      RaptorDinosaurController controller = new RaptorDinosaurController(redTRexModel,view,positionToInstatiate,waypoints);
   }
   
}
