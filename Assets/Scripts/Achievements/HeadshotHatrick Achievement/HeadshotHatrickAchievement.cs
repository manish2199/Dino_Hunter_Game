using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadshotHatrickAchievement : Achievement
{    
    public HeadshotHatrickAchievement()
    { 
        Counter = 0; 
        achievementType = AchievementType.HatrickOfHeadShots;  
    }

    public HeadshotHatrickAchievement(RaptorsType raptorsType, int headshotNumber ,  UnlockWeaponType unlockWeaponType, Sprite unclockWeapon)
	{
        Counter = 0; 
        achievementType = AchievementType.HatrickOfHeadShots;  
        RaptorsType = raptorsType;
        HeadshotNumber = headshotNumber;
        UnlockWeaponType = unlockWeaponType;
        UnlockWeaponIcon = unclockWeapon;
	}

    public RaptorsType RaptorsType;

    public int HeadshotNumber; 

    public UnlockWeaponType UnlockWeaponType;

    public Sprite UnlockWeaponIcon; 

    private WalkableDinosaurView WalkableDinosaurView; 

	public override void Subscribe()
	{
       SetAchievementText();
	   DinosaurBody.OnHeadshot += CheckDinosaurType;	
       DinosaurBody.OnBodyShot += ResetCounters;
       ShootableWeaponController.OnMissTarget += ResetCounters;
	}

	public override void UnSubscribe()
	{
	   DinosaurBody.OnHeadshot -= CheckDinosaurType;
       DinosaurBody.OnBodyShot -= ResetCounters;
       ShootableWeaponController.OnMissTarget -= ResetCounters;
	}

    private void CheckDinosaurType(RaptorsType raptorType, WalkableDinosaurView walkableDinosaurView)
    { 
       if(RaptorsType == raptorType)
       {
           WalkableDinosaurView = walkableDinosaurView;

           if(WalkableDinosaurView != walkableDinosaurView)
           {
               ResetCounters();
            }

           UpdateAchievement();
       }
    }


	protected override void UpdateAchievement()
	{
		Counter ++;
      
        if(Counter >= HeadshotNumber)
        {
            InvokeAchievementAcomplished(this);
        }

	}

    protected virtual void SetAchievementText()
    {
       Achievementtext = "Hit Consecutive"+HeadshotNumber+"HeadShot On "+RaptorsType + " Unlock Weapon - ";
    }
}


public enum UnlockWeaponType
{
    None,
    ShotGun,
    AssualtRifle,
}