using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Math_Calculations;

[RequireComponent(typeof(NavMeshAgent))]
public class WalkableDinosaurStates : MonoBehaviour
{
   public WalkableDinosaurView walkableDinosaurView;

   protected NavMeshAgent aiAgent;

   protected Animator animator;

   protected Transform PlayerTarget;

   protected void Awake()
   {  
      walkableDinosaurView = GetComponent<WalkableDinosaurView>();

      aiAgent = GetComponent<NavMeshAgent>(); 

      animator = GetComponent<Animator>();
       
      PlayerTarget = Player.Instance.playerTransform;
   }

   protected Vector3 GetDirection(Vector3 firstPosition , Vector3 secondPosition)
   { 
      return ( secondPosition - firstPosition );
   }


   protected float GetDistance(Vector3 firstPosition , Vector3 secondPosition)
   {
      return CustomMathFunctions.Distance(firstPosition, secondPosition);
   }


   public virtual void OnStateUpdate(){}

   public virtual void OnStateEnter()
   {
      this.enabled = true;
   }

   public virtual void OnStateExit()
   {
      this.enabled = false;
   }
}

