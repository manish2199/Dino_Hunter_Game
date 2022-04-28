using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSoundController : MonoBehaviour
{
  // job is to control player audio

  float MinFootStepVolume { get; set; }
  float MaxFootStepVolume { get; set; }
  float StepDistance { get; set; }
  float AccumulatedStepDistance { get; set; }

  private int EnemyCount = 0;  

  public void IntializeSoundSetting(AudioSource GameplayMusicAudio,PlayerScriptableObject playerScriptableObject)
  {
    PlayGamePlaySound();

    SetInitialFootStepAudio(playerScriptableObject);
  }

  public void OnEnable()
  {
     PlayerMovementController.OnSprinting += SetSprintAudio;
     PlayerMovementController.OnCrouched += SetCrouchedAudio;
     PlayerMovementController.OnWalking += SetWalkingAudio;

     EnemiesService.OnPlayerDetected += PlayBattleSound;
     EnemiesService.OnEnemyDead += CheckForGamePlaySound;
     InventoryService.OnItemCollected += PlayItemCollectedSound;
  }

   public void OnDisable()
  {
     PlayerMovementController.OnSprinting -= SetSprintAudio;
     PlayerMovementController.OnCrouched -= SetCrouchedAudio;
     PlayerMovementController.OnWalking -= SetWalkingAudio;

     EnemiesService.OnPlayerDetected -= PlayBattleSound;
     EnemiesService.OnEnemyDead -= CheckForGamePlaySound;
     InventoryService.OnItemCollected -= PlayItemCollectedSound;
  }


  public void StopPlayerAudios()
  {
    Player.Instance.GameplayMusicAudioSource.Stop();
  }

  private void PlayItemCollectedSound(NotificationType other)
  {
    if(!Player.Instance.EffectsAudioSource.isPlaying)
    {
        Player.Instance.EffectsAudioSource.clip =  Player.Instance.PlayerScriptableObject.ItemCollectedClip;
        Player.Instance.EffectsAudioSource.Play();
    }
  }

  void CheckForGamePlaySound()
  { 
    if(EnemyCount > 0)
    {
      EnemyCount --;
    }

    if(EnemyCount <= 0 )
    {
       PlayGamePlaySound();
    }

  }

  public void PlayGamePlaySound()
  {
       Player.Instance.GameplayMusicAudioSource.Stop();
       Player.Instance.GameplayMusicAudioSource.clip = Player.Instance.PlayerScriptableObject.GamePlayAudioClip;
       Player.Instance.GameplayMusicAudioSource.Play();
  }


  void PlayBattleSound()
  { 
    EnemyCount ++; 
    if( Player.Instance.GameplayMusicAudioSource.clip = Player.Instance.PlayerScriptableObject.GamePlayAudioClip)
    {
       Player.Instance.GameplayMusicAudioSource.Stop();
       Player.Instance.GameplayMusicAudioSource.clip =  Player.Instance.PlayerScriptableObject.BattleAudioClip;
       Player.Instance.GameplayMusicAudioSource.Play();
    }
  }
 

  public void SetWalkingAudio(PlayerScriptableObject playerScriptableObject)
  {
        MinFootStepVolume = playerScriptableObject.minWalkVolume;
        MaxFootStepVolume = playerScriptableObject.maxWalkVolume;
        StepDistance = playerScriptableObject.walkStepDistance;
  }

   public void SetCrouchedAudio(PlayerScriptableObject playerScriptableObject)
  { 
        MinFootStepVolume =playerScriptableObject.crouhVolume;
        MaxFootStepVolume = playerScriptableObject.crouhVolume;
        StepDistance =playerScriptableObject.crouchStepDistance;
  }

   public void SetSprintAudio(PlayerScriptableObject playerScriptableObject)
  {
        MinFootStepVolume = playerScriptableObject.sprintVolume;
        MaxFootStepVolume = playerScriptableObject.sprintVolume;
        StepDistance =playerScriptableObject.sprintStepDistance;
  }

  
  public void PlayFootStepAudio(CharacterController characterController,PlayerScriptableObject playerScriptableObject,AudioSource playerFootStepAudio)
  {
     if(!characterController.isGrounded)
     { 
       return;
     }

     if(characterController.velocity.sqrMagnitude > 0 )
     {
        AccumulatedStepDistance += Time.deltaTime;

        if(AccumulatedStepDistance > StepDistance)
        {
           playerFootStepAudio.volume = Random.Range(MinFootStepVolume,MaxFootStepVolume);
           playerFootStepAudio.clip = playerScriptableObject.FootStepClips[Random.Range(0 ,playerScriptableObject.FootStepClips.Length)];
           playerFootStepAudio.Play();

           AccumulatedStepDistance = 0f;
         }
      }
     else
     {
        AccumulatedStepDistance = 0f;
     }
  }

  public void SetInitialFootStepAudio(PlayerScriptableObject playerScriptableObject)
  {
    StepDistance= playerScriptableObject.walkStepDistance;
    MinFootStepVolume = playerScriptableObject.minWalkVolume;
    MaxFootStepVolume = playerScriptableObject.maxWalkVolume;
   }

}