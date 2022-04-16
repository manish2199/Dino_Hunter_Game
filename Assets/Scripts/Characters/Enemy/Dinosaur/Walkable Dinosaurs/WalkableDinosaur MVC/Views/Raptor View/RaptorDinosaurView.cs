using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RaptorDinosaurView : WalkableDinosaurView
{
  public ParticleSystem ParticleEffect;  
  
  public LayerMask PlayerLayerMask;

  public Transform ProjectilePos;

  private IEnumerator DeathCoroutine;

  public bool IsDead;
 

  void Start()
  {
      RaptorDinosaurController temp = (RaptorDinosaurController)walkableDinosaurController;
      temp.SetInitialState();
  }

  void Update()
  {  
     if(!IsDead) 
     { 
         walkableDinosaurController.CurrentState.OnStateUpdate();
     }
  }

  public void EnableDinosaur()
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
      
  public void DisableTheDinosaur()
  {
      RaptorDinosaurController temp = (RaptorDinosaurController)walkableDinosaurController;

      RaptorDinosaurPool.Instance.ReturnItem(temp);
      EnemiesService.Instance.StartTimerForRaptors(temp);

      DeathCoroutine = Death(); 
      StartCoroutine(Death());
  }

  IEnumerator Death()
  {
      IsDead = true;

      animator.SetTrigger("Death"); 
  
      yield return new WaitForSeconds(1f);
      
      gameObject.SetActive(false);
  }

  public void stopCoroutine()
  {
      StopCoroutine(DeathCoroutine);
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
        
        
        
        
        
        
        
        
        
        
        
        // Bit shift the index of the layer (8) to get a bit mask
      //   int layerMask = 1 << 8;

      //   // This would cast rays only against colliders in layer 8.
      //   // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
      //   layerMask = ~layerMask;

      //   RaycastHit hit;
      //   // Does the ray intersect any objects excluding the player layer
      //   if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
      //   {
      //       Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
      //       Debug.Log("Did Hit");
      //   }
      //   else
      //   {
      //       Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
      //       Debug.Log("Did not Hit");
      //   }