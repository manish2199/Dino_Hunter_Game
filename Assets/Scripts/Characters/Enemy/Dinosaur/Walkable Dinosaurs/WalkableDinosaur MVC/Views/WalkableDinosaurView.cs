using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class WalkableDinosaurView : MonoBehaviour 
{
	public Animator animator;

	public AudioSource AudioSource;

   public NavMeshAgent navMeshAgent;

	[SerializeReference]public WalkableDinosaurController walkableDinosaurController; 

   [SerializeReference]public PatrollingState PatrollingState;

   [SerializeReference]public AttackState AttackingState;

   [SerializeReference]public ChasingState ChasingState;

   public Transform DinosaurTransform; 

   protected IEnumerator DeathCoroutine;

  [HideInInspector] public bool IsDead;

  protected virtual void Start()
  {
     walkableDinosaurController.SetInitialState();
  }

  protected virtual void Update()
  {
     if(!IsDead) 
     { 
         walkableDinosaurController.CurrentState.OnStateUpdate();
     }
  }

  public virtual void EnableDinosaur(){}
  
   public virtual void DisableTheDinosaur(){}

   protected virtual IEnumerator Death()
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


  public virtual void  PlaySound(AudioClipType audioClipType)
  {
      AudioClip[]  audioClips =  walkableDinosaurController.SearchAudio(audioClipType);
	  
  	   int randomNum = Random.Range(0,audioClips.Length);
     
	   AudioSource.clip = audioClips[randomNum];

	   AudioSource.Play();
  }
  
}

public enum DinosaurBodyPartType
{
	None,
	Head,
	MainBody,
}


