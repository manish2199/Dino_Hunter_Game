using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : WalkableDinosaurStates
{

   protected WalkableDinosaurModel WalkableDinosaurModel;
    
   protected bool isAlreadyAttack;  

   protected float TimeBetweenAttack = 0.7f ;


  public override void OnStateUpdate()
  {
     if(!isAlreadyAttack)
     {
        PerformAttack();
     }

		if(PlayerTarget != null && GetDistance(transform.position,PlayerTarget.position) > WalkableDinosaurModel.AttackingRange )
     {
         walkableDinosaurView.walkableDinosaurController.ChangeState(walkableDinosaurView.ChasingState);
     }

     	if(PlayerTarget == null)
		{
			walkableDinosaurView.walkableDinosaurController.ChangeState(walkableDinosaurView.PatrollingState);
		}

   }

   protected virtual void PerformAttack() 
  { 
       animator.SetTrigger("Attack");
       animator.transform.LookAt(PlayerTarget);
       isAlreadyAttack = true;
       Invoke(nameof(ResetAttack),TimeBetweenAttack);
  }


   protected virtual void ResetAttack()
   {
      isAlreadyAttack = false;	
      // animator.ResetTrigger("Attack");	
   }


   public override void OnStateEnter()
   {
      base.OnStateEnter(); 

      isAlreadyAttack = false;

      WalkableDinosaurModel = walkableDinosaurView.walkableDinosaurController.WalkableDinosaurModel;

   }

   public override void OnStateExit()
   {
      base.OnStateExit();

      WalkableDinosaurModel = null;
    
      isAlreadyAttack = true;  

      TimeBetweenAttack = 0f;
     
      if(aiAgent.enabled == true)
		{
         aiAgent.destination = Vector3.zero;
		   aiAgent.ResetPath();
		}

   }
}
