using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : WalkableDinosaurStates
{
    [SerializeReference] protected WalkableDinosaurModel WalkableDinosaurModel;

	public override void OnStateUpdate()
	{ 
		if(aiAgent.enabled == true)
		{
		    aiAgent.SetDestination(PlayerTarget.position);

	    	animator.transform.LookAt(PlayerTarget);
		}
		if(GetDistance(transform.position,PlayerTarget.position) < WalkableDinosaurModel.AttackingRange )
        {
			walkableDinosaurView.walkableDinosaurController.ChangeState(walkableDinosaurView.AttackingState);
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
