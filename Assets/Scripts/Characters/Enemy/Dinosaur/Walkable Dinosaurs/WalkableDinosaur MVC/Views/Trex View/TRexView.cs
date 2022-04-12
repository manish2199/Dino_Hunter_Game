using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexView : WalkableDinosaurView
{
    // patrolling state is different than raptors 
    // attacking state is same 
    // chasing state is slightly different

    // public TRexPatrollingState TRexPatrollingState; 
    
    // public AttackState AttackingState; 

    // public TRexChasingState TRexChasingState; 

  void Start()
  {
     walkableDinosaurController.SetInitialState();
  }


  void Update()
  {
     walkableDinosaurController.CurrentState.OnStateUpdate();
  }


   public virtual void PlaySound(AudioClipType audioClipType)
   {
      AudioClip[]  audioClips =  walkableDinosaurController.SearchAudio(audioClipType);
	  
  	   int randomNum = Random.Range(0,audioClips.Length);
     
	   AudioSource.clip = audioClips[randomNum];

	   AudioSource.Play();
   } 
 


}

