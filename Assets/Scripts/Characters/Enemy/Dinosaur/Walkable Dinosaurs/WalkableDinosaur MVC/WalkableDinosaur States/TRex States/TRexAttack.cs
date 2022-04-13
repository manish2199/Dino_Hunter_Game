using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexAttack : AttackState
{ 
  protected IEnumerator TrexAttackCoroutine;
  
  public override void OnStateUpdate()
  {
     if(!isAlreadyAttack)
     {
        TrexAttackCoroutine = PerformTRexAttack();
        StartCoroutine(PerformTRexAttack());
     }

		if(GetDistance(transform.position,PlayerTarget.position) > WalkableDinosaurModel.AttackingRange )
     {
         walkableDinosaurView.walkableDinosaurController.ChangeState(walkableDinosaurView.ChasingState);
     }

   }

   protected IEnumerator PerformTRexAttack() 
  { 
       animator.SetTrigger("Attack");
       animator.transform.LookAt(PlayerTarget);
       isAlreadyAttack = true;

       yield return new WaitForSeconds(2.5f);  
     
       isAlreadyAttack = false;
    }


    public override void OnStateEnter()
    {
       base.OnStateEnter();

       TimeBetweenAttack = 3f;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
   
}
