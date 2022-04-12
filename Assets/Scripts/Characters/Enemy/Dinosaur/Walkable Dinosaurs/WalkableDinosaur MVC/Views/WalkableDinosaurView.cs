using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class WalkableDinosaurView : MonoBehaviour 
{
	public Animator animator;

	public AudioSource AudioSource;

	[SerializeReference]public WalkableDinosaurController walkableDinosaurController; 

    public PatrollingState PatrollingState;

    public AttackState AttackingState;

    public ChasingState ChasingState;


   public virtual void Start()
   {
     walkableDinosaurController.SetInitialState();
   }


  public virtual void Update()
   {
     walkableDinosaurController.CurrentState.OnStateEnter();
      
   }

  
}

public enum DinosaurBodyPartType
{
	None,
	Head,
	MainBody,
}


