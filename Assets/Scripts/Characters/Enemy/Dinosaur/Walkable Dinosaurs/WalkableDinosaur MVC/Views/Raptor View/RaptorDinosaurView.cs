using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RaptorDinosaurView : WalkableDinosaurView
{
  public ParticleSystem ParticleEffect;  


  void Start()
  {
     walkableDinosaurController.SetInitialState();
  }


  void Update()
  {
     walkableDinosaurController.CurrentState.OnStateUpdate();
  }
 

  public void PerformSpecialAbility()
  {
       RaptorDinosaurController temp = (RaptorDinosaurController)walkableDinosaurController;
  
       temp.PerformSpecialAbility();
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