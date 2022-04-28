using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "AchievementsScriptableObject", menuName = "ScriptableObject/NewAchievementsScriptableObject")] 
public class AchievementsScriptableObject : ScriptableObject
{
   [SerializeField] AchievementCell[] achievementCellList;

    private void OnValidate()
    { 
        ValidateAllAchievementCell();        
    } 

    public void SubscribeAchievements()
    {
        for ( int i =0; i<achievementCellList.Length; i++)
        {
            achievementCellList[i].SubscribeAchievement();
        }
    }

    public void UnSubscribeAllAchievements()
    {
       for ( int i =0; i<achievementCellList.Length; i++)
        {
            achievementCellList[i].UnSubscribeAchievement();
        }
    }

    void ValidateAllAchievementCell()
    {
        if(achievementCellList != null)
        {
           for( int i =0; i<achievementCellList.Length; i++)
           {
              achievementCellList[i].Validate();
           }
        }
    }
}

