using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementService : GenericSingleton<AchievementService>
{
   [SerializeField] AchievementsScriptableObject achievementsList;  




   protected override void Awake()
   {
      if(Instance == null)
      {
        Instance = this;
      }
   }

   private void OnEnable()
   {
      achievementsList.SubscribeAchievements();
   
   }

   private void OnDisable()
   {
      achievementsList.UnSubscribeAllAchievements();
   }

 
}
