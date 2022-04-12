using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableDinosaurController 
{
    public WalkableDinosaurController(WalkableDinosaurModel walkableDinosaurModel,WalkableDinosaurView view,Transform positionToInstantiate, Transform[] wayPoints ) 
    {
        WalkableDinosaurModel = walkableDinosaurModel;
        
        WalkableDinosaurModel.WayPoints = wayPoints;

        WalkableDinosaurView = GameObject.Instantiate<WalkableDinosaurView>(view,positionToInstantiate);

        WalkableDinosaurModel.PlayerTarget = (Player.Instance.PlayerTarget != null) ? Player.Instance.PlayerTarget : null ;  // (Caution)

    }

   [SerializeReference] public WalkableDinosaurModel WalkableDinosaurModel;

   [SerializeReference] public WalkableDinosaurView  WalkableDinosaurView ;

    public WalkableDinosaurStates CurrentState { get; set; } 

    public virtual void SetInitialState()
    {
       ChangeState(WalkableDinosaurView.PatrollingState);        
    }

    public virtual void ChangeState(WalkableDinosaurStates state)
    {
        if(CurrentState != null)
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
  

    
}


public class RaptorDinosaurController : WalkableDinosaurController
{
    public RaptorDinosaurController(RaptorDinosaurModel raptorDinosaurModel,RaptorDinosaurView view,Transform positionToInstantiate, Transform[] wayPoints ) : base (raptorDinosaurModel,view,positionToInstantiate,wayPoints)
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

}


public class TRexDinosaurController : WalkableDinosaurController
{

    public TRexDinosaurController(TRexDinosaurModel model,TRexView view,Transform positionToInstantiate, Transform[] wayPoints ) : base (model,view,positionToInstantiate,wayPoints) 
    {
      
    }

    // TRexDinosaurModel  TRexDinosaurModel { get; }

    // TRexDinosaurView  TRexDinosaurView { get; }
   

}
