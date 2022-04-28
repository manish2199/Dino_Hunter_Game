using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRaptorPatrolling : PatrollingState 
{
 
   public override void OnStateUpdate()
   {
       base.OnStateUpdate();
   }
   

   protected override IEnumerator PatrollingRoutine() 
    {
        aiAgent.isStopped = true;
        aiAgent.velocity = Vector3.zero; 

       yield return new WaitForSeconds(1.5f);

       animator.SetTrigger("Sniff");

       yield return new WaitForSeconds(3f);
        
        animator.SetTrigger("Search");

       yield return new WaitForSeconds(2.3f);
         
        animator.SetTrigger("Attack");
    
       yield return new WaitForSeconds(2.3f);
      
       animator.ResetTrigger("Attack");
        if(aiAgent.enabled == true)
        {
        IterateToNextWayPoint();
        aiAgent.isStopped = false;     
        CanPatrol = true;
        SetWayPointDestination();
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
