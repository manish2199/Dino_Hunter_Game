using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexPatrollingState : PatrollingState
{
    protected void Awake()
    {
        base.Awake();
    }
 
   public override void OnStateUpdate()
   {        
        if(CanPatrol )
        {
            if(GetDistance(transform.position,CurrentWayPointTarget) < aiAgent.stoppingDistance ) 
            { 
                CanPatrol = false; 
                animator.SetBool("Walk",false);
                PatrolCoroutine = this.PatrollingRoutine();
                StartCoroutine(PatrolCoroutine);      
           }
           CheckEnemyDetection(); 
        } 

     
   }


   protected override IEnumerator EnemyDetectedRoutine()
  {
      animator.transform.LookAt(PlayerTarget);
      aiAgent.isStopped = true;
      aiAgent.velocity = Vector3.zero; 
      animator.SetBool("Walk",false);
    
      yield return new WaitForSeconds(1f);

      animator.SetTrigger("Roar");

      yield return new WaitForSeconds(3.5f); 

      animator.ResetTrigger("Roar");
      walkableDinosaurView.walkableDinosaurController.ChangeState(walkableDinosaurView.ChasingState);
  }




   protected override IEnumerator PatrollingRoutine() 
    {
        aiAgent.isStopped = true;
        aiAgent.velocity = Vector3.zero; 

       yield return new WaitForSeconds(1.5f);

       animator.SetTrigger("Sniff");

       yield return new WaitForSeconds(1.5f);
      
       animator.SetTrigger("EarClean");

       yield return new WaitForSeconds(1.5f);

       animator.SetTrigger("BodyShake");
       
       yield return new WaitForSeconds(4f);

       animator.SetTrigger("Roar");

       yield return new WaitForSeconds(3.5f); 
       
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
