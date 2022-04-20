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

   private void EnableAchievement()
   {
      achievementsList.SubscribeAchievements();
   }


   private void DisableAllAchievements()
   {
      achievementsList.UnSubscribeAllAchievements();
   }

 
}
