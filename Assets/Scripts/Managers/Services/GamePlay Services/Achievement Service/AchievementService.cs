using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementService : GenericSingleton<AchievementService>
{
   [SerializeField] AchievementsScriptableObject achievementsList;  

   private void OnEnable()
   {
      achievementsList.SubscribeAchievements();
   
   }

   private void OnDisable()
   {
      achievementsList.UnSubscribeAllAchievements();
   }

 
}
