using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WalkableDinosaurStates : MonoBehaviour
{
  [SerializeReference] public WalkableDinosaurView walkableDinosaurView;

   [SerializeField]protected NavMeshAgent aiAgent;

   [SerializeField]protected Animator animator;

   protected Transform PlayerTarget;

   protected void Awake()
   {
      //  walkableDinosaurView = GetComponent<WalkableDinosaurView>();
      
       aiAgent = GetComponent<NavMeshAgent>();

      // animator = walkableDinosaurView.animator; 

      PlayerTarget = Player.Instance.playerTransform;
   }

   protected Vector3 GetDirection(Vector3 firstPosition , Vector3 secondPosition)
   { 
      return ( secondPosition - firstPosition );
   }


   protected float GetDistance(Vector3 firstPosition , Vector3 secondPosition)
   {
       // return distance between dinosaur and CURRENT target  WAYpoint 

       Vector3 heading ;
       float distance; 
       float distanceSquared;

        heading.x = secondPosition.x - firstPosition.x;
        heading.y = secondPosition.y - firstPosition.y;
        heading.z = secondPosition.z - firstPosition.z;
        
        distanceSquared = (heading.x *  heading.x) + ( heading.y *  heading.y ) + ( heading.z * heading.z);
        distance = Mathf.Sqrt(distanceSquared);

        return distance;
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


// // Base State 
// public class RaptorStates : Monobehaviour 
// {
//    
// }
