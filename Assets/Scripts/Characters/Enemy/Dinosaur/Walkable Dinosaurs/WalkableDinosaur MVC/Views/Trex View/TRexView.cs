using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexView : WalkableDinosaurView
{ 
   [HideInInspector]  public bool IsDead;

   private IEnumerator DeathCoroutine;
    

   void Start()
   {
     TRexDinosaurController temp = (TRexDinosaurController)walkableDinosaurController;
      temp.SetInitialState();
   }


  
   public void EnableDinosaur()
  {
      gameObject.SetActive(true);
      if(walkableDinosaurController != null && walkableDinosaurController.CurrentState != null)
      {
         walkableDinosaurController.ChangeState(PatrollingState);  
      }
  }
  

  void Update()
  {
     if(!IsDead)
     {
       walkableDinosaurController.CurrentState.OnStateUpdate();
     }
  }


  public void DisableTheDinosaur()
  {
      TRexDinosaurController temp = (TRexDinosaurController)walkableDinosaurController;

      TRexDinosaurPool.Instance.ReturnItem(temp);
      EnemiesService.Instance.StartTimerForTRex(temp);

      DeathCoroutine = Death(); 
      StartCoroutine(Death());
  }

  IEnumerator Death()
  {
     // play death anim
       IsDead = true;

      animator.SetTrigger("Death"); 
  
      yield return new WaitForSeconds(1f);
      
      gameObject.SetActive(false);
  }

   public void stopCoroutine()
  {
      StopCoroutine(DeathCoroutine);
  }



   public virtual void PlaySound(AudioClipType audioClipType)
   {
      AudioClip[]  audioClips =  walkableDinosaurController.SearchAudio(audioClipType);
	  
  	   int randomNum = Random.Range(0,audioClips.Length);
     
	   AudioSource.clip = audioClips[randomNum];

	   AudioSource.Play();
   } 
 
}




 