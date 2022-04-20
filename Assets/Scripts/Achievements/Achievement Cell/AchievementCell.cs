using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AchievementCell 
{
    [SerializeField] AchievementType achievementType = AchievementType.None;
    
    [SerializeReference] Achievement achievementConstraints;

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
        if(achievementType != lastType)
        {
            lastType = achievementType;
            achievementConstraints = UpdateAchievement(lastType);
        }
    }

    private Achievement UpdateAchievement(AchievementType achievementType)
    {
        Achievement newAchievement = null; 

        if(achievementType == AchievementType.HatrickOfHeadShots)
        { 
            newAchievement = new HeadshotHatrickAchievement();
        }  
        if(achievementType == AchievementType.TRexKill)
        {
            newAchievement = new TRexKillAchievement();
        }    
        
        return newAchievement;
    }
}

