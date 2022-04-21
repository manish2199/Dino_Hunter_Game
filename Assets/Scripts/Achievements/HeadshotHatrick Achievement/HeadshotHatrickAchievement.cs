using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadshotHatrickAchievement : Achievement
{       
    public RaptorsType RaptorsType;

    public int HeadshotNumber; 

    public UnlockWeaponType UnlockWeaponType;

    public Sprite UnlockWeaponIcon; 

    private WalkableDinosaurView WalkableDinosaurView; 

    public HeadshotHatrickAchievement() : base () {}

	public override void Subscribe()
	{
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
           if(Counter == 0 )
           {
               WalkableDinosaurView = walkableDinosaurView;
           }
           if(WalkableDinosaurView != walkableDinosaurView)
           {
               ResetCounters();
               WalkableDinosaurView = walkableDinosaurView;
            }

           UpdateAchievement();
       }
    }


	protected override void UpdateAchievement()
	{
		Counter ++ ;
        
        if(Counter == HeadshotNumber)
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