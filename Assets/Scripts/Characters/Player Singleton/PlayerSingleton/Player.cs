using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GenericSingleton<Player>
{
    [SerializeField] private PlayerScriptableObject playerScriptableObject;
    public PlayerScriptableObject PlayerScriptableObject { get { return playerScriptableObject; }}
     
    public Transform rootTransform;

    public Transform playerTransform;

    public Transform PlayerCollectableTransform;

    public Transform weaponsHolder;

    public CharacterController  characterController;

    [SerializeField] PlayerMovementController playerMovementController;

    public PlayerMouseLookController playerMouseLookController;

    [SerializeField] PlayerSoundController playerSoundController;

    public PlayerAttackController playerAttackController;

    public PlayerStatsController playerStatsController;

    [SerializeField] PlayerAnimationController playerAnimationController;
    
    public bool IsCrouched { get; set; }    

    public bool IsSprinting { get; set; } 

    public bool IsWalking { get; set; }   

    public bool IsZoomed { get; set; }


   protected override void Awake()
   { 
      if(Instance == null)
      {
         Instance = this;
      }
   }
    
    
    void OnEnable()
    {
       WeaponService.OnWeaponZoomIn += PlayerCameraZoomInAinmation;       
    }

    void OnDisable()
    {
       WeaponService.OnWeaponZoomIn -= PlayerCameraZoomInAinmation;       
    } 

    public void PlayerCameraZoomInAinmation(bool canZoomIn)
    { 
      playerAnimationController.CamZoom(canZoomIn);
    }

    public void SetWalkingFootStepClips()
    {
       playerSoundController.SetWalkingAudio();
    }

    public void SetCrouchedFootStepClips()
    {
       playerSoundController.SetCrouchedAudio();
    }

    public void SetSprintFootStepClips()
    {
       playerSoundController.SetSrintAudio();
    }

    public void TakeDamage(int damage)
    {
        playerStatsController.TakeDamage(damage);
    }

    void Update()
    {
       playerMovementController.PlayerMovement();
     
       playerMouseLookController.MouseLookAround(); 
          
       playerSoundController.PlayFootStepAudio();
   
       playerAttackController.SelectWeapon();
    }       
}
