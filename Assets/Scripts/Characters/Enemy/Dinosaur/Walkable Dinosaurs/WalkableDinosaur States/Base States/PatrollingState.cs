using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math_Calculations;


public class PatrollingState : WalkableDinosaurStates
{  
  protected bool CanPatrol; 

  protected int CurrentWayPointIndex = 0; 

  protected Transform[] WayPoints;
   
  protected Vector3 CurrentWayPointTarget; 

  [SerializeReference,HideInInspector]protected WalkableDinosaurModel WalkableDinosaurModel;


   protected float ChasingRange;

   protected float AttackingRange; 

   protected IEnumerator PatrolCoroutine;

   protected IEnumerator EnemyDetectionCoroutine; 


   public override void OnStateUpdate()
   {
      if(CanPatrol)
      {
          if(GetDistance(transform.position,CurrentWayPointTarget) < 0.6f )  
        {   
            CanPatrol = false; 
            animator.SetBool("Walk",false);
            PatrolCoroutine = PatrollingRoutine();
            StartCoroutine(PatrolCoroutine);      
        }
        CheckEnemyDetection(); 
      }
   }
   

   protected virtual void CheckEnemyDetection()
   {
       // if enemy detected return true
       if(PlayerTarget && Player.Instance.gameObject.activeInHierarchy )
       {
        Vector3 targetDirection = GetDirection(transform.position , PlayerTarget.position );

        if(CustomMathFunctions.IsPresentInFront(transform.forward,targetDirection))
        {
          if( GetDistance(transform.position,PlayerTarget.position) < ChasingRange )
          {
              // DIRECT ATTACK
              EnemyDetected();
          }
        }
       }
  }

  public void EnemyDetected()
  {
    EnemyDetectionCoroutine = EnemyDetectedRoutine();   
    StartCoroutine(EnemyDetectionCoroutine);
  }



  protected virtual IEnumerator EnemyDetectedRoutine()
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




  protected virtual IEnumerator PatrollingRoutine() 
    {
        aiAgent.isStopped = true;
        aiAgent.velocity = Vector3.zero; 

       yield return new WaitForSeconds(1.5f);

       animator.SetTrigger("Sniff");

       yield return new WaitForSeconds(3f);
        
        animator.SetTrigger("Search");

       yield return new WaitForSeconds(2.3f);
         
        if(aiAgent.enabled == true)
        {
        IterateToNextWayPoint();
        aiAgent.isStopped = false;     
        CanPatrol = true;
        SetWayPointDestination();
        }
    }

   
  


   public virtual void SetWayPointDestination()
   {
      CurrentWayPointTarget = WayPoints[CurrentWayPointIndex].position;
      

      if(CanPatrol && aiAgent.isOnNavMesh == true )
       {
         aiAgent.SetDestination(CurrentWayPointTarget);
         animator.SetBool("Walk",true);
       }
   }

   protected virtual void IterateToNextWayPoint()
   {
       int prevIndex = CurrentWayPointIndex;
       do
       {
          CurrentWayPointIndex = Random.Range(0,WayPoints.Length-1);
       }while(prevIndex == CurrentWayPointIndex);
      
   }
  

	public override void OnStateEnter()
	{
		base.OnStateEnter();

        CanPatrol = true;  


        WalkableDinosaurModel = walkableDinosaurView.walkableDinosaurController.WalkableDinosaurModel;

        WayPoints = WalkableDinosaurModel.WayPoints;
       
       
        aiAgent.speed = WalkableDinosaurModel.Speed;

        aiAgent.angularSpeed = WalkableDinosaurModel.AngularSpeed;

        aiAgent.acceleration = WalkableDinosaurModel.Acceleration ;

        aiAgent.angularSpeed  = WalkableDinosaurModel.AngularSpeed;

        aiAgent.stoppingDistance = WalkableDinosaurModel.StoppingDistanceFromWayPoint;

        SetWayPointDestination();
       

        ChasingRange = WalkableDinosaurModel.ChasingRange;

        AttackingRange = WalkableDinosaurModel.AttackingRange;
       
      
	}



	public override void OnStateExit()
	{
		base.OnStateExit();

        EnemiesService.Instance.InvokeOnPlayerDetected();
         
        CanPatrol = false;

      if(PatrolCoroutine != null && EnemyDetectionCoroutine!=null)
      { 
        StopCoroutine(PatrolCoroutine);

        StopCoroutine(EnemyDetectionCoroutine);
      }
  
        CurrentWayPointIndex = 0; 
   
        CurrentWayPointTarget = Vector3.zero; 

        WalkableDinosaurModel = null;

        ChasingRange = 0;

        AttackingRange  = 0; 

        PatrolCoroutine = null;

        EnemyDetectionCoroutine = null;

        aiAgent.destination = Vector3.zero;

	}
}




























//  IEnumerator Pat()
//     {
//         canStart = false;  
//        aiAgent.isStopped = true;
//       aiAgent.velocity = Vector3.zero;

//         yield return new WaitForSeconds(2.5f);
          
//         raptorDinosaurView.animator.SetTrigger("Sniff");

//         yield return new WaitForSeconds(1f);  

//         IterateToNextWayPoint();
//         IterateToNextWayPoint();
//         aiAgent.isStopped = false;
//         SetWayPointDestination();

//         yield return new WaitForSeconds(1f);  

//         SetWayPointDestination();

//     }