using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RaptorDinosaurView : WalkableDinosaurView
{
  public ParticleSystem ParticleEffect;  
  
  public LayerMask PlayerLayerMask;

  public Transform ProjectilePos;
 

  protected override void Start()
  {
      base.Start();
  }
 
  protected override void Update()
  {  
    base.Update();
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
      if(ParticleEffect!= null && ParticleEffect.isPlaying)
      {
          ParticleEffect.Stop();
      }
    }
  }
      
  public override void DisableTheDinosaur()
  {
      RaptorDinosaurController temp = (RaptorDinosaurController)walkableDinosaurController;

      RaptorDinosaurPool.Instance.ReturnItem(temp);
      EnemiesService.Instance.StartTimerForRaptors(temp);

      DeathCoroutine = Death(); 
      StartCoroutine(Death());
  }

  public void PerformSpecialAbility()
  {
       RaptorDinosaurController temp = (RaptorDinosaurController)walkableDinosaurController;
  
       temp.PerformSpecialAbility(); 
  }

  public void RayCastForSpecialAbility()
  {
      RaptorDinosaurController temp = (RaptorDinosaurController)walkableDinosaurController;

      temp.RayCastForSpecialAbility();
  }

  public void StopSpecialAbility()
  {
     ParticleEffect.Stop();
  }

   public override void PlaySound(AudioClipType audioClipType)
   {
      AudioClip[]  audioClips =  walkableDinosaurController.SearchAudio(audioClipType);
	  
  	   int randomNum = Random.Range(0,audioClips.Length);
     
	   AudioSource.clip = audioClips[randomNum];

	   AudioSource.Play();
   } 
}








// Raptors States 
// RaptorPatrol   ( Patrolling State - walk , idle , snif , look around )  [ Repeated Behaviour ]
// RaptorChase    ( directlly jumps from patrol )
// RaptorAttack   ( can jump form chase or direct from patrol but to goes to onlu to patrol )





// Task 1 Create Patrolling Raptor (Patrolling State)
// Task 2 Create Chasing Raptor (Chasing State)
// Task 3 Create Attacking State ( Attacking State)
        
        
        
        
        
        
        
        
        
        
