using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WalkableDinosaurRespawner
{
   [SerializeReference] public WalkableDinosaurController WalkableDinosaurController;
   public int TimeToRespawn;

   public async void SetTimer()
   {
      TimeToRespawn = WalkableDinosaurController.WalkableDinosaurModel.TimeToRespawnAfterDeath;

      await StartRespawnTimer(); 
   }

   async Task StartRespawnTimer()
   {
      await Task.Delay(TimeToRespawn);
      
      TimerFinished();
   }

   private void TimerFinished()
   {
      EnemiesService.Instance.RespawnDiosaur(WalkableDinosaurController);
   }     

}





