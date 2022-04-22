using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public abstract class Achievement
{
    public Achievement() { Counter = 0; }

    [HideInInspector] protected int Counter;
    
    protected string Achievementtext;

    public string AchievementText { get { return Achievementtext; } }

    [HideInInspector] public AchievementType achievementType;

    public static event Action<Achievement> OnAchievementAcomplished;
    
    public abstract void Subscribe();

    public abstract void UnSubscribe();

    protected void InvokeAchievementAcomplished(Achievement achievement)
    {
        OnAchievementAcomplished?.Invoke(achievement);
    }

    protected virtual void UpdateAchievement()
    {} 

    protected virtual void SetAchievementText()
    {}

    protected virtual void ResetCounters()
    {
        Counter = 0;
    }

} 


public enum AchievementType
{
    None,
    HatrickOfHeadShots,
    TRexKill,
}


