using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexChasingState : ChasingState
{


    protected void Awake()
    {
        base.Awake();
    }

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
	}

	public override void OnStateExit()
	{
		base.OnStateExit();
	}
}
