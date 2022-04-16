using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexAttack : AttackState
{ 
  float Timer = 0;

  float NextTimeToAttack = 1.5f;
  
  public override void OnStateUpdate()
  {
      Timer += Time.deltaTime;

      if(Timer >= NextTimeToAttack)
      {
         animator.SetTrigger("Attack");
         animator.transform.LookAt(PlayerTarget);
         Timer = 0f;
      }

		if(GetDistance(transform.position,PlayerTarget.position) > WalkableDinosaurModel.AttackingRange )
     {
         walkableDinosaurView.walkableDinosaurController.ChangeState(walkableDinosaurView.ChasingState);
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
