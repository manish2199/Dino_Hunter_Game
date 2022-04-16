using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class WalkableDinosaurView : MonoBehaviour 
{
	public Animator animator;

	public AudioSource AudioSource;

	[SerializeReference]public WalkableDinosaurController walkableDinosaurController; 

   [SerializeReference]public PatrollingState PatrollingState;

   [SerializeReference]public AttackState AttackingState;

   [SerializeReference]public ChasingState ChasingState;

   public Transform DinosaurTransform; 

  public virtual void Start()
  {
     walkableDinosaurController.SetInitialState();
  }

  public virtual void Update()
  {
     walkableDinosaurController.CurrentState.OnStateEnter();
  }

  public virtual void  PlaySound(AudioClipType audioClipType){}
  
}

public enum DinosaurBodyPartType
{
	None,
	Head,
	MainBody,
}


