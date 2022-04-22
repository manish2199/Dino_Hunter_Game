using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : WalkableDinosaurStates
{
    [SerializeReference] protected WalkableDinosaurModel WalkableDinosaurModel;

	public override void OnStateUpdate()
	{ 
		if(aiAgent.enabled == true && PlayerTarget != null)
		{ 
		    aiAgent.SetDestination(PlayerTarget.position);

	    	animator.transform.LookAt(PlayerTarget);
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
    
		WalkableDinosaurModel = walkableDinosaurView.walkableDinosaurController.WalkableDinosaurModel;     

        animator.SetBool("Walk",true);
		aiAgent.isStopped = false;
          
        aiAgent.stoppingDistance = WalkableDinosaurModel.TargetStopppingDistance;

	}

	public override void OnStateExit()
	{
		base.OnStateExit();

		animator.SetBool("Walk",false);
        
        if(aiAgent.enabled == true)
		{
          aiAgent.isStopped = true;
          aiAgent.destination = Vector3.zero;
		  aiAgent.ResetPath();
		}
		WalkableDinosaurModel = null;
   
	}
}
