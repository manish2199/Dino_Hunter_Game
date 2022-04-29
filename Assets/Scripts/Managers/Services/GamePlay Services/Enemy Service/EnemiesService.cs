using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 


public class EnemiesService : GenericSingleton<EnemiesService>
{ 
   [SerializeField] RaptorDinosaurPool RaptorDinosaurPool;

   [SerializeField] TRexDinosaurPool TRexDinosaurPool;
 
   [SerializeField] WalkableDinosaurSpawner[] WalkableDinosaursList;

   public static event Action OnPlayerDetected; 

   public static event Action OnEnemyDead;


   public void InvokeOnPlayerDetected()
   {
      OnPlayerDetected?.Invoke();
   } 

   public void InvokeOnEnemyDead()
   {
      OnEnemyDead?.Invoke();
   } 



   
   // private List<WalkableDinosaurRespawner> WalkableDinosaurRespawners; 
 
   protected override void Awake()
   {
      if(Instance == null)
      {
         Instance = this;
      }
      // WalkableDinosaurRespawners = new List<WalkableDinosaurRespawner>();
   }

   void Start()
   {
      SpawnWalkableDinosaur();      
   } 

   public void StartTimerForRaptors(RaptorDinosaurController raptorDinosaurController)
   { 
      WalkableDinosaurRespawner newWalkableDinosaurRespawner = new WalkableDinosaurRespawner();
      newWalkableDinosaurRespawner.WalkableDinosaurController = raptorDinosaurController;
      newWalkableDinosaurRespawner.SetTimer();
      // WalkableDinosaurRespawners.Add(newWalkableDinosaurRespawner);
   }

   public void StartTimerForTRex(TRexDinosaurController trexDinosaurController)
  { 
      WalkableDinosaurRespawner newWalkableDinosaurRespawner = new WalkableDinosaurRespawner();
      newWalkableDinosaurRespawner.WalkableDinosaurController = trexDinosaurController;
      newWalkableDinosaurRespawner.SetTimer();
      // WalkableDinosaurRespawners.Add(newWalkableDinosaurRespawner);
  }

   public void SpawnWalkableDinosaur()
   {
      for(int i= 0 ; i<WalkableDinosaursList.Length; i++)
      {
         // spawn every dinosaur in array 
         // set its position 
         if(WalkableDinosaursList[i].WalkableDinosaurScriptableObject.WalkingDinosaurType == WalkingDinosaurType.Raptors)
         {
            // create raptor 
           RaptorDinosaurModel raptorModel = new RaptorDinosaurModel(WalkableDinosaursList[i].WalkableDinosaurScriptableObject);

           Raptor raptor = (Raptor)WalkableDinosaursList[i].WalkableDinosaurScriptableObject.WalkingDinosaurs;

           RaptorDinosaurView view =(RaptorDinosaurView)raptor.WalkableDinosaurView;
 
           RaptorDinosaurController controller = RaptorDinosaurPool.GetRaptorDinosaur(raptorModel,view); 

           // Wrap Position 
           if( view.navMeshAgent.isOnNavMesh == false)
           {
              view.navMeshAgent.Warp(WalkableDinosaursList[i].PositionToInstantiate.position);
           }
               
           controller.SetWayPoint(WalkableDinosaursList[i].WayPoints);
         }
         if(WalkableDinosaursList[i].WalkableDinosaurScriptableObject.WalkingDinosaurType == WalkingDinosaurType.TRex)
         {
            // create Trex
            TRexDinosaurModel trexModel = new TRexDinosaurModel(WalkableDinosaursList[i].WalkableDinosaurScriptableObject);

            TRex trex = (TRex)WalkableDinosaursList[i].WalkableDinosaurScriptableObject.WalkingDinosaurs;

            TRexView view =(TRexView)trex.WalkableDinosaurView;
 
            TRexDinosaurController controller = TRexDinosaurPool.GetTRexDinosaur(trexModel,view); 
            if( view.navMeshAgent.isOnNavMesh == false)
           {
              view.navMeshAgent.Warp(WalkableDinosaursList[i].PositionToInstantiate.position);
           }
            // controller.SetPosition(WalkableDinosaursList[i].PositionToInstantiate);
            controller.SetWayPoint(WalkableDinosaursList[i].WayPoints);
         }
         
      }
   }



   public void RespawnDiosaur(WalkableDinosaurController walkableDinosaurController)
   {
      // // check its type 
      if(walkableDinosaurController.WalkableDinosaurModel.WalkingDinosaurType == WalkingDinosaurType.Raptors)
      { 
         RaptorDinosaurController raptorDinosaurController = (RaptorDinosaurController)walkableDinosaurController;

         RespawnRaptors(raptorDinosaurController);

      }
      if(walkableDinosaurController.WalkableDinosaurModel.WalkingDinosaurType == WalkingDinosaurType.TRex)
      {
         TRexDinosaurController trexDinosaurController = (TRexDinosaurController)walkableDinosaurController;

         RespawnTRex(trexDinosaurController);
      }
   }

   public void RespawnTRex(TRexDinosaurController TRexDinosaurController)
   {
      TRexDinosaurModel trexModel = (TRexDinosaurModel)TRexDinosaurController.WalkableDinosaurModel; 

      TRexView view =(TRexView)TRexDinosaurController.WalkableDinosaurView;
 
      TRexDinosaurController controller = TRexDinosaurPool.GetTRexDinosaur(trexModel,view); 
      controller.EnableDinosaur();
   
      // RemoveRespawner(TRexDinosaurController);
     
   }

   public void RespawnRaptors(RaptorDinosaurController raptorDinosaurController )
   { 
      RaptorDinosaurModel raptorModel = (RaptorDinosaurModel)raptorDinosaurController.WalkableDinosaurModel; 

      RaptorDinosaurView view =(RaptorDinosaurView)raptorDinosaurController.WalkableDinosaurView;
 
      RaptorDinosaurController controller = RaptorDinosaurPool.GetRaptorDinosaur(raptorModel,view); 
      controller.EnableDinosaur();

      // RemoveRespawner(raptorDinosaurController);
   }

   // public void RemoveRespawner(WalkableDinosaurController walkableDinosaurController)
   // {
   //    for(int i=0; i<WalkableDinosaurRespawners.Count; i++)
   //    {
   //       if(WalkableDinosaurRespawners[i].WalkableDinosaurController == walkableDinosaurController)
   //       {
   //          WalkableDinosaurRespawners.Remove(WalkableDinosaurRespawners[i]);
   //       }
   //    }
   // }



   public void ClearTheDinosaurs()
   {
      // remove all dinosaur controller before exit scene
   }

   



   

}


[System.Serializable]
public class WalkableDinosaurSpawner
{
   public WalkableDinosaurScriptableObject WalkableDinosaurScriptableObject;
   public Transform PositionToInstantiate;
   public Transform[] WayPoints;  
}





// Jobs to do 
// 1. Spawn the enemies when start 
// 2. if particular enemy die then disable it return it to the pool and again spawn it after certain time 
//3. when player want to exit clean the pool 