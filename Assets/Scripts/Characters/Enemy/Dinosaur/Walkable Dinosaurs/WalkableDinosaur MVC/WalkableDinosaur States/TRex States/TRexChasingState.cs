using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexChasingState : ChasingState
{
    float timeToGrowl = 1.5f;

    bool isAlreadyGrowl;

      protected void Awake()
    {
        base.Awake();
    }

	public override void OnStateUpdate()
	{
		aiAgent.SetDestination(PlayerTarget.position);

		animator.transform.LookAt(PlayerTarget);
        

        if(!isAlreadyGrowl)
        {
           animator.SetTrigger("WalkGrowl");
           Invoke(nameof(Reset),timeToGrowl);     
        }

       
       
        
		if(GetDistance(transform.position,PlayerTarget.position) < WalkableDinosaurModel.AttackingRange )
       {
			walkableDinosaurView.walkableDinosaurController.ChangeState(walkableDinosaurView.AttackingState);
		}
	} 



    void Reset()
    {
      isAlreadyGrowl = false;
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
