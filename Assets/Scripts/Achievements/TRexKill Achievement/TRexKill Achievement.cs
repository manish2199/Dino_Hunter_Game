using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexKillAchievement : Achievement
{
    public TRexKillAchievement() : base ()
    {}

   public int NumberOfKill;   

	public override void Subscribe()
	{
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
       Achievementtext = "Killed "+NumberOfKill +" Number of TRex";
    }
  
}
