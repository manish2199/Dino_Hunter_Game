using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexChasingState : ChasingState
{
   private float Timer = 0f;
   private float NextRoaringTime = 10f;

	public override void OnStateUpdate()
	{ 
		if(aiAgent.enabled == true)
		{
		    aiAgent.SetDestination(PlayerTarget.position);

	    	animator.transform.LookAt(PlayerTarget);

            Timer += Time.deltaTime;

            if(Timer > NextRoaringTime)
            {
                animator.SetTrigger("WalkGrowl");
                Timer = 0f;
            } 

             
        }

		if(PlayerTarget != null && GetDistance(transform.position,PlayerTarget.position) < WalkableDinosaurModel.AttackingRange )
        {
			walkableDinosaurView.walkableDinosaurController.ChangeState(walkableDinosaurView.AttackingState);
		}

		if(PlayerTarget == null)
		{
			walkableDinosaurView.walkableDinosaurController.ChangeState(walkableDinosaurView.PatrollingState);
		}
	
	}

	

	public override void OnStateEnter()
	{
		base.OnStateEnter();
	}

	public override void OnStateExit()
	{
		base.OnStateExit();
	}
    
}
