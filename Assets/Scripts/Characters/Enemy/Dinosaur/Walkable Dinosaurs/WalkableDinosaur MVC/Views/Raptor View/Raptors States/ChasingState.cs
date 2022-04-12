using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : WalkableDinosaurStates
{
    [SerializeReference] private WalkableDinosaurModel WalkableDinosaurModel;

	public override void OnStateUpdate()
	{
		aiAgent.SetDestination(PlayerTarget.position);

		animator.transform.LookAt(PlayerTarget);

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

        aiAgent.isStopped = true;
	}
}
