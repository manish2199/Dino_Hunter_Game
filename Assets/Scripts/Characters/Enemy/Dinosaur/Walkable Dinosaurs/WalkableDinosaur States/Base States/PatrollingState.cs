using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  TO patroll need waypoints , aigent , target transform
public class PatrollingState : WalkableDinosaurStates
{  
  protected bool CanPatrol; 

  protected int currentWayPointIndex = 0; 

  protected Transform[] WayPoints;
   
  protected Vector3 CurrentWayPointTarget; 

  [SerializeReference,HideInInspector]protected WalkableDinosaurModel WalkableDinosaurModel;

   protected float FieldOfViewAnle;

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
            PatrolCoroutine = Coroutine();
            StartCoroutine(PatrolCoroutine);      
        }
        CheckEnemyDetection(); 
      }
   }
   

   protected virtual void CheckEnemyDetection()
   {
       // if enemy detected return true
        Vector3 targetDirection = GetDirection(transform.position , PlayerTarget.position );
        
        float targetAngle = Vector3.Angle(transform.forward , targetDirection );

        if(targetAngle < ( FieldOfViewAnle / 2) )
        {
          if( GetDistance(transform.position,PlayerTarget.position) < ChasingRange )
          {
              // DIRECT ATTACK
              EnemyDetectionCoroutine = EnemeDetected();
              StartCoroutine(EnemyDetectionCoroutine);
          }
          
        }
   }

   protected virtual IEnumerator EnemeDetected()
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




   protected virtual IEnumerator Coroutine() 
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
      CurrentWayPointTarget = WayPoints[currentWayPointIndex].position;


      if(aiAgent.enabled == true && animator != null && CanPatrol )
      {
         aiAgent.SetDestination(CurrentWayPointTarget);
         animator.SetBool("Walk",true);
      }
   }

   protected virtual void IterateToNextWayPoint()
   {
    //   currentWayPointIndex ++;
    //   if( currentWayPointIndex == WayPoints.Length)
    //   {
    //       currentWayPointIndex = 0;
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

        CanPatrol = true;  

        WalkableDinosaurModel = walkableDinosaurView.walkableDinosaurController.WalkableDinosaurModel;

        WayPoints = WalkableDinosaurModel.WayPoints;
       
        SetWayPointDestination();

       aiAgent.speed = WalkableDinosaurModel.Speed;

       aiAgent.angularSpeed = WalkableDinosaurModel.AngularSpeed;

       aiAgent.acceleration = WalkableDinosaurModel.Acceleration ;

       aiAgent.angularSpeed  = WalkableDinosaurModel.AngularSpeed;

       aiAgent.stoppingDistance = WalkableDinosaurModel.StoppingDistanceFromWayPoint;

       FieldOfViewAnle = WalkableDinosaurModel.FieldOfViewAnle;

       ChasingRange = WalkableDinosaurModel.ChasingRange;

       AttackingRange = WalkableDinosaurModel.AttackingRange;
	}



	public override void OnStateExit()
	{
		base.OnStateExit();
         
        CanPatrol = false;
        

        if(PatrolCoroutine != null && EnemyDetectionCoroutine != null)
        {
           StopCoroutine(PatrolCoroutine);

           StopCoroutine(EnemyDetectionCoroutine);
        }
 
        currentWayPointIndex = 0; 
   
        CurrentWayPointTarget = Vector3.zero; 

        WalkableDinosaurModel = null;

        FieldOfViewAnle = 0;

       ChasingRange = 0;

       AttackingRange  = 0; 

       PatrolCoroutine = null;

       EnemyDetectionCoroutine = null;

       aiAgent.destination = Vector3.zero;

	}
}

     
//       async Task MoveToNextWayPoint()
//    {
//         IterateToNextWayPoint();
        
//         if(aiAgent != null)
//         {
//           aiAgent.isStopped = false;
//         }
//         await Task.Delay(1600);
//         canStart = true;
//         SetWayPointDestination();
//    }




    //  public async Task PatrolRoutine() 
    // {
    //     await StopRaptor();

    //     await Task.Delay(2000);

    //     await PlaySniffAnim();

    //     await Task.Delay(1500);

    //     await PlayRoarAnim();

    //     await Task.Delay(1500); 

    //     await MoveToNextWayPoint();
    // }


//    async Task StopRaptor()
//    {
//       if(aiAgent != null)
//       {
//       aiAgent.isStopped = true;
//       aiAgent.velocity = Vector3.zero;
//       }
//    }

//    async Task PlaySniffAnim()
//    {  
//        if(animator != null)
//        {
//          animator.SetTrigger("Sniff");
//        }
//    }

   
//     async Task PlayRoarAnim()
//    {  
//        if(animator != null)
//        {
//         //  raptorDinosaurView.animator.ResetTrigger("Sniff");
//          animator.SetTrigger("Roar");
//        }
//    }




























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