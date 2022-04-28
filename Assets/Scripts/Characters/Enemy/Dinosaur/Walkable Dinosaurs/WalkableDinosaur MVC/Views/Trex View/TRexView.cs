using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexView : WalkableDinosaurView
{ 
    
   protected override void Start()
   {
      base.Start();
   }


  
   public override void EnableDinosaur()
  {
     if(!gameObject.activeInHierarchy)
     {
      gameObject.SetActive(true);
      if(walkableDinosaurController != null && walkableDinosaurController.CurrentState != null)
      {
         walkableDinosaurController.ChangeState(PatrollingState);  
      }
     }
  }
  

  protected override void Update()
  {
      base.Update();
  }


  public override void DisableTheDinosaur()
  {
      TRexDinosaurController temp = (TRexDinosaurController)walkableDinosaurController;

      TRexDinosaurPool.Instance.ReturnItem(temp);
      EnemiesService.Instance.StartTimerForTRex(temp);

      DeathCoroutine = Death(); 
      StartCoroutine(Death());
  }

   protected IEnumerator Death()
  {
     // play death anim
       IsDead = true;

      animator.SetTrigger("Death"); 
  
      yield return new WaitForSeconds(2.5f);
      
      gameObject.SetActive(false);
  }
 
}




 