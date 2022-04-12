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
       print(GetDistance(transform.position,PlayerTarget.position));
     
        if(GetDistance(transform.position,CurrentWayPointTarget) < aiAgent.stoppingDistance && CanPatrol )  
        { 
            CanPatrol = false; 
            animator.SetBool("Walk",false);
            PatrolCoroutine = this.Coroutine();
            StartCoroutine(PatrolCoroutine);      
        }
        CheckEnemyDetection(); 
   }
   

   protected override void CheckEnemyDetection()
   {
       // if enemy detected return true
        Vector3 targetDirection = GetDirection(transform.position , PlayerTarget.position );
        
        float targetAngle = Vector3.Angle(transform.forward , targetDirection );

        if(targetAngle < ( FieldOfViewAnle / 2) )
        {
          if( GetDistance(transform.position,PlayerTarget.position) < ChasingRange )
          {
              // DIRECT ATTACK
              EnemyDetectionCoroutine = this.EnemeDetected();
              StartCoroutine(EnemyDetectionCoroutine);
          }
          
        }
   }

   protected override IEnumerator EnemeDetected()
  {
      animator.transform.LookAt(PlayerTarget);
      aiAgent.isStopped = true;
      aiAgent.velocity = Vector3.zero; 
      animator.SetBool("Walk",false);
    
      yield return new WaitForSeconds(1f);

      animator.SetTrigger("Roar");

      yield return new WaitForSeconds(1f); 

      walkableDinosaurView.walkableDinosaurController.ChangeState(walkableDinosaurView.ChasingState);
  }




   protected override IEnumerator Coroutine() 
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

        this.IterateToNextWayPoint();
        aiAgent.isStopped = false;     
        CanPatrol = true;
        SetWayPointDestination();
    }

   
  


   protected override void IterateToNextWayPoint()
   {
    //   currentWayPointIndex ++;
    //   if( currentWayPointIndex == WayPoints.Length)
    //   {
        //   currentWayPointIndex = 0;
    //   }

       int prevIndex = currentWayPointIndex;
       do
       {
          currentWayPointIndex = Random.Range(0,WayPoints.Length-1);
       }while(prevIndex == currentWayPointIndex);
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
