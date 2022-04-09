using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
  // job is to control player audio
 
//   private PlayerScriptableObject playerScriptableObject ;
  
  float MinFootStepVolume { get; set; }
  float MaxFootStepVolume { get; set; }
  float StepDistance { get; set; }
  float AccumulatedStepDistance { get; set; }

  [SerializeField] AudioSource playerFootStepAudio;


  void Start()
  {
      // playerScriptableObject  = Player.Instance.PlayerScriptableObject;
      SetInitialFootStepAudio();
  }

  public void SetWalkingAudio()
  {
        MinFootStepVolume = Player.Instance.PlayerScriptableObject.minWalkVolume;
        MaxFootStepVolume = Player.Instance.PlayerScriptableObject.maxWalkVolume;
        StepDistance =Player.Instance.PlayerScriptableObject.walkStepDistance;
  }

   public void SetCrouchedAudio()
  { 
        MinFootStepVolume = Player.Instance.PlayerScriptableObject.crouhVolume;
        MaxFootStepVolume = Player.Instance.PlayerScriptableObject.crouhVolume;
        StepDistance =Player.Instance.PlayerScriptableObject.crouchStepDistance;
  }

   public void SetSrintAudio()
  {
      MinFootStepVolume = Player.Instance.PlayerScriptableObject.sprintVolume;
        MaxFootStepVolume = Player.Instance.PlayerScriptableObject.sprintVolume;
        StepDistance =Player.Instance.PlayerScriptableObject.sprintStepDistance;
  }

  
  public void PlayFootStepAudio()
  {
     if(!Player.Instance.characterController.isGrounded)
     { 
       return;
     }

     if(Player.Instance.characterController.velocity.sqrMagnitude > 0 )
     {
        AccumulatedStepDistance += Time.deltaTime;

        if(AccumulatedStepDistance > StepDistance)
        {
           playerFootStepAudio.volume = Random.Range(MinFootStepVolume,MaxFootStepVolume);
           playerFootStepAudio.clip = Player.Instance.PlayerScriptableObject.FootStepClips[Random.Range(0 ,Player.Instance.PlayerScriptableObject.FootStepClips.Length)];
           playerFootStepAudio.Play();

           AccumulatedStepDistance = 0f;
         }
      }
     else
     {
        AccumulatedStepDistance = 0f;
     }
   }

  public void SetInitialFootStepAudio()
  {
    StepDistance= Player.Instance.PlayerScriptableObject.walkStepDistance;
    MinFootStepVolume = Player.Instance.PlayerScriptableObject.minWalkVolume;
    MaxFootStepVolume = Player.Instance.PlayerScriptableObject.maxWalkVolume;
   }

}