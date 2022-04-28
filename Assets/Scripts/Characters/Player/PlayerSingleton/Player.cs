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

   public PlayerMovementController playerMovementController;

   public PlayerMouseLookController playerMouseLookController;

   public PlayerSoundController playerSoundController;

   public PlayerAttackController playerAttackController;

   public PlayerStatsController playerStatsController;

   public PlayerAnimationController playerAnimationController;

   public Animator PlayerAnimator;
    
   public AudioSource FootStepAudioSource;

   public AudioSource GameplayMusicAudioSource;

   public AudioSource EffectsAudioSource;



   protected override void Awake()
   { 
      // base.Awake();
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

   public void TakeDamage(int damage)
   {
        playerStatsController.TakeDamage(damage);
   }
   




   //  Update Functions
   public void PlayerMovement()
   {
         playerMovementController.MovePlayer(playerScriptableObject,characterController,playerTransform);
      
         playerMovementController.HandleCrouch(playerScriptableObject,rootTransform);
   }


   private void MouseLookAround()
   {
      playerMouseLookController.LockAndUnlockCursor(playerScriptableObject);
 
      if(playerMouseLookController.isCursorLocked())
     {
        playerMouseLookController.LookAround(playerScriptableObject,rootTransform,playerTransform);
      }
   } 


   private void PlayerStatsUpdates()
   {
      playerStatsController.CheckToOpenInventory(playerScriptableObject);

      playerStatsController.CheckToCollectSupplies(PlayerCollectableTransform,playerScriptableObject);

      playerStatsController.CheckToUseMedikit(playerScriptableObject);
   }


   
   void Start()
   {
      playerMovementController.SetInitialMovementSetup(playerScriptableObject);

      playerMouseLookController.InitializeMouseLook();

      playerSoundController.IntializeSoundSetting(GameplayMusicAudioSource,playerScriptableObject);

      playerAttackController.SelectInitialWeapon(); 

      playerStatsController.InitializePlayerStats(playerScriptableObject);
   }

   void Update()
    {
       PlayerMovement();
     
       MouseLookAround(); 
          
       playerSoundController.PlayFootStepAudio(characterController,playerScriptableObject,FootStepAudioSource);
   
       playerAttackController.SelectWeapon(playerScriptableObject,weaponsHolder);

       PlayerStatsUpdates();
    }       
}
