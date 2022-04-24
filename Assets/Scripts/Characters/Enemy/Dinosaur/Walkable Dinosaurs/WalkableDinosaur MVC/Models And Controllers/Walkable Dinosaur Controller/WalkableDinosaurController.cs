using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableDinosaurController 
{  
    [SerializeReference] public WalkableDinosaurModel WalkableDinosaurModel;

    [SerializeReference] public WalkableDinosaurView  WalkableDinosaurView ;

    public WalkableDinosaurStates CurrentState { get; set; }  


    public WalkableDinosaurController(WalkableDinosaurModel walkableDinosaurModel,WalkableDinosaurView view ) 
    {
        WalkableDinosaurModel = walkableDinosaurModel;
        
        WalkableDinosaurView = GameObject.Instantiate<WalkableDinosaurView>(view);

        WalkableDinosaurModel.HealhToReduce = WalkableDinosaurModel.Health;
    }
     
    public void SetPosition(Transform positionToInstantiate)
    {
        WalkableDinosaurView.DinosaurTransform.position = positionToInstantiate.position;
    }
  
    public void SetWayPoint(Transform[] Waypoints)
    {
        WalkableDinosaurModel.WayPoints = Waypoints;
    }

    public virtual void SetInitialState()
    {
       ChangeState(WalkableDinosaurView.PatrollingState);        
    }

    public virtual void ChangeState(WalkableDinosaurStates state)
    {  
         
        if(CurrentState != null )
        { 
            CurrentState.OnStateExit();
        }
       
        CurrentState = state;
        CurrentState.OnStateEnter();
    }

    public AudioClip[] SearchAudio(AudioClipType audioClipType )
    {
        AudioClip[] temp = null;
        for(int i = 0; i<WalkableDinosaurModel.DinosaurAudios.Length; i++)
        {
            if(audioClipType == WalkableDinosaurModel.DinosaurAudios[i].AudioClipType )
            {
               temp = WalkableDinosaurModel.DinosaurAudios[i].AudioClip;
            }
        }
        return temp;
    }

    public virtual void TakeDamage(int damage){}

 
   // Receive the damage from Dinosaur body part and decide whether dinosaur is alive or not 
   // if health is less than zero then add this controller to object bool by resetting all variables in view mostly reset aipath

    
}


public class RaptorDinosaurController : WalkableDinosaurController
{
    public RaptorDinosaurController(RaptorDinosaurModel raptorDinosaurModel,RaptorDinosaurView view ) : base (raptorDinosaurModel,view)
    {
        WalkableDinosaurView.walkableDinosaurController = this;
      
    }


    public void PerformSpecialAbility()
    {
        RaptorDinosaurModel model = (RaptorDinosaurModel)WalkableDinosaurModel;

        RaptorDinosaurView view = (RaptorDinosaurView)WalkableDinosaurView; 

        view.ParticleEffect.gameObject.SetActive(true);

        if(model.SpecialAbility.specialAbilityType != SpecialAbilityType.None)
        {  
            view.ParticleEffect.Play();


        }
    }

    public void RayCastForSpecialAbility()
    {
        RaptorDinosaurModel model = (RaptorDinosaurModel)WalkableDinosaurModel;

        RaptorDinosaurView view = (RaptorDinosaurView)WalkableDinosaurView; 


        if(model.SpecialAbility.specialAbilityType != SpecialAbilityType.None)
       {  
          RaycastHit hit;
          if(Physics.Raycast(view.ProjectilePos.position,view.ProjectilePos.forward,out hit,model.TargetStopppingDistance,view.PlayerLayerMask))
          {     
            IDamagable damagable = hit.transform.gameObject.GetComponent<IDamagable>();

            if(damagable!= null)
            {
                damagable.TakeDamage(model.Damage);
            }

          }   
      }
      
    } 

    public override void TakeDamage(int damage)
    {
        if(WalkableDinosaurModel.HealhToReduce > 0)
        { 
          WalkableDinosaurModel.HealhToReduce -= damage;
        }
        if(WalkableDinosaurModel.HealhToReduce <= 0)
        { 
        //    SetInitialState();
           RaptorDinosaurView view = (RaptorDinosaurView)WalkableDinosaurView; 
           
           view.DisableTheDinosaur();
           
           EnemiesService.Instance.InvokeOnEnemyDead();

           Player.Instance.playerStatsController.IncreaseScore(WalkableDinosaurModel.WalkingDinosaurType);
        }
    }

    

    public void EnableDinosaur()
    {
        RaptorDinosaurView view = (RaptorDinosaurView)WalkableDinosaurView; 
         
        ResetHealth(); 

        view.EnableDinosaur();
 
        view.IsDead = false;
    }

    public void ResetHealth()
    {
        RaptorDinosaurModel model = (RaptorDinosaurModel)WalkableDinosaurModel;

        model.HealhToReduce = model.Health;
    }

}


public class TRexDinosaurController : WalkableDinosaurController
{ 

    public static event Action OnTrexDeath;

    public TRexDinosaurController(TRexDinosaurModel model,TRexView view) : base (model,view) 
    {
       WalkableDinosaurView.walkableDinosaurController = this;
    }

    public override void TakeDamage(int damage)
    {
        if(WalkableDinosaurModel.HealhToReduce > 0)
        { 
          WalkableDinosaurModel.HealhToReduce -= damage;
        }
        if(WalkableDinosaurModel.HealhToReduce <= 0)
        { 
        //    SetInitialState();
           OnTrexDeath?.Invoke();
           TRexView view = (TRexView)WalkableDinosaurView; 
           
           view.DisableTheDinosaur(); 

           Player.Instance.playerStatsController.IncreaseScore(WalkableDinosaurModel.WalkingDinosaurType);
        }
    }

    

    public void EnableDinosaur()
    {
        TRexView view = (TRexView)WalkableDinosaurView; 
         
        ResetHealth(); 

        view.EnableDinosaur();
 
        view.IsDead = false;
    }

    public void ResetHealth()
    {
        TRexDinosaurModel model = (TRexDinosaurModel)WalkableDinosaurModel;

        model.HealhToReduce = model.Health;
    }

}