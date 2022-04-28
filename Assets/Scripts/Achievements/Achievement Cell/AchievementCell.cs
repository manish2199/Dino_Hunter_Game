using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AchievementCell 
{
    [SerializeField] AchievementType achievementType;
    
    [SerializeReference] Achievement achievementConstraints;
  
    private Achievement savedAchievement;

    private AchievementType lastType;

    public AchievementCell()
    {
        lastType = achievementType; 
    }

    public void SubscribeAchievement()
    {
        achievementConstraints.Subscribe();
    }

    public void UnSubscribeAchievement()
    {
        achievementConstraints.UnSubscribe();
    }

    public void Validate()
    {
        if(lastType != achievementType )
        { 
            lastType = achievementType;       
            savedAchievement = achievementConstraints;

            achievementConstraints = UpdateAchievement(savedAchievement);
            savedAchievement = null;
        }
    }

    private Achievement UpdateAchievement(Achievement other)
    {
        Achievement newAchievement = null; 

        if(achievementType != AchievementType.None && achievementType == AchievementType.HatrickOfHeadShots)
        { 
            var temp = (HeadshotHatrickAchievement)other;
            newAchievement = (other!=null) ? new HeadshotHatrickAchievement(temp.RaptorsType,temp.HeadshotNumber,temp.UnlockWeaponType,temp.UnlockWeaponIcon) : new HeadshotHatrickAchievement();
        }  
        if(achievementType != AchievementType.None && achievementType == AchievementType.TRexKill)
        {
            var temp = (TRexKillAchievement)other;
            newAchievement = (other!=null) ? new TRexKillAchievement(temp.HealthKitsMaxLimit,temp.NumberOfKill) : new TRexKillAchievement();
        }    
        
        return newAchievement;
    }
}

