using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexKillAchievement : Achievement
{
    public TRexKillAchievement() 
    { 
		Counter = 0;
		achievementType = AchievementType.TRexKill; 
	}

	public TRexKillAchievement(int healthKitsMaxLimit, int numberOfKill)
	{
	    Counter = 0;
		achievementType = AchievementType.TRexKill; 	
        HealthKitsMaxLimit = healthKitsMaxLimit;
		NumberOfKill = numberOfKill;
	}
    
    public int HealthKitsMaxLimit;

	public HealthKitType HealthKitType;

    public int NumberOfKill;   

	public override void Subscribe()
	{
		SetAchievementText();
	   TRexDinosaurController.OnTrexDeath += UpdateAchievement; 
	}

	public override void UnSubscribe()
	{
	   TRexDinosaurController.OnTrexDeath -= UpdateAchievement;  
	}

	protected override void UpdateAchievement()
	{
		Counter ++ ;
        
        if(Counter == NumberOfKill)
        {
            InvokeAchievementAcomplished(this);
        }

	}

    protected virtual void SetAchievementText()
    {
       Achievementtext = "Killed "+NumberOfKill +" Number of TRex. HealthKit MaxLimit Has Been Increased ";
    }
  
}
