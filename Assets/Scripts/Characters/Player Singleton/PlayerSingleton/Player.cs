using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :  GenericSingleton<Player>
{
    [SerializeField] private PlayerScriptableObject playerScriptableObject;
    public PlayerScriptableObject PlayerScriptableObject { get { return playerScriptableObject; }}
     
    public Transform rootTransform;

    public Transform playerTransform;

    public Transform PlayerTarget;

    public Transform weaponsHolder;

    public CharacterController  characterController;

    [SerializeField] PlayerMovementController playerMovementController;

    [SerializeField] PlayerMouseLookController playerMouseLookController;

    [SerializeField] PlayerSoundController playerSoundController;

    [SerializeField] PlayerAttackController playerAttacksController;

    // [SerializeField] PlayerStatsController PlayerStatsController;

    [SerializeField] PlayerAnimationController playerAnimationController;
    
    public bool IsCrouched { get; set; }    

    public bool IsSprinting { get; set; } 

    public bool IsWalking { get; set; }   

    public bool IsZoomed { get; set; }


   protected override void Awake()
   { 
      base.Awake();
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



    void Update()
    {
       playerMovementController.PlayerMovement();
     
       playerMouseLookController.MouseLookAround(); 
          
       playerSoundController.PlayFootStepAudio();
   
       playerAttacksController.SelectWeapon();
    }       
}
