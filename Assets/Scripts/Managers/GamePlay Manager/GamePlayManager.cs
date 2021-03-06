using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : GenericSingleton<GamePlayManager>
{   
   [SerializeField] LevelInitializerScriptableObject InitialLevelConstraints;
  
   [SerializeField] Transform PlayerRespawnposition;

   [SerializeField] GameObject GameOverCamera; 

   public static event Action OnPlayerDeath;

   public static event Action OnGameRestart;
   
   protected override void Awake()
   {
      base.Awake();

      InitialSetup();
   }

   void Start()
   {
        Player.Instance.gameObject.transform.position = PlayerRespawnposition.position;
   }

  private void OnEnable()
  {
      Achievement.OnAchievementAcomplished += UnlockItems; 
   }

  private void OnDisable()
  {
      Achievement.OnAchievementAcomplished -= UnlockItems;
  }

     
  private void UnlockItems(Achievement achievement)
  {   
     if(achievement.achievementType == AchievementType.HatrickOfHeadShots)
     {  
        // Check unlock weapon type then unlcok it
         var headshotHatrickAchievement = (HeadshotHatrickAchievement)achievement;
      
        if(headshotHatrickAchievement.UnlockWeaponType == UnlockWeaponType.ShotGun)
         {
            // Unlock the weapon and Save Data
            GameData.SetShotgunUnlocked(1); 
            WeaponService.Instance.UnlockTheWeapon(WeaponsID.ShotGun); 
            AddProjectilesToInventory(ProjectileType.ShotGunBullet);

            // Show the ui text
         }
         if(headshotHatrickAchievement.UnlockWeaponType == UnlockWeaponType.AssualtRifle)
         {
          
            // Unlock the weapon and Save Data
            GameData.SetAssualtRifleUnlocked(1); 
            WeaponService.Instance.UnlockTheWeapon(WeaponsID.AssaultRifle); 
            AddProjectilesToInventory(ProjectileType.AssaultRifleBullet);
            
            // sHOW UI TEXT 
         }
         headshotHatrickAchievement.UnSubscribe(); 
         NotificationManager.Instance.ShowAchievementComplete(headshotHatrickAchievement.AchievementText,headshotHatrickAchievement.UnlockWeaponIcon);
     }
     if(achievement.achievementType == AchievementType.TRexKill)
     {
        var trexKillAchievement = (TRexKillAchievement)achievement;

        InventoryService.Instance.IncreaseHealthKitsMaxLimit(trexKillAchievement.HealthKitsMaxLimit,trexKillAchievement.HealthKitType);  
        achievement.UnSubscribe();
     } 
  } 



   public void UnlockRevolver()
   {
      WeaponService.Instance.UnlockTheWeapon(WeaponsID.Revolver);
      AddProjectilesToInventory(ProjectileType.RevolverBullet);
      Player.Instance.playerAttackController.InitialWeaponSetup();
   }


   private void InitialSetup()
   { 
      // Revolver Is Always Unlocked 
      UnlockRevolver();

      //Add Initial Medikit to Inventory
      AddInitialMediKitsToInventory();

      // Check If Which Weapons Are Locked 
      if(GameData.GetShotgunUnlocked() == 1)
      {
         WeaponService.Instance.UnlockTheWeapon(WeaponsID.ShotGun); 
         // Add Initial Unlocked Inventory Projectiles to Inventory 
         AddProjectilesToInventory(ProjectileType.ShotGunBullet);
      }
      if(GameData.GetAssualtRifleUnlocked() == 1)
      {
         WeaponService.Instance.UnlockTheWeapon(WeaponsID.AssaultRifle);  

         // Add Initial Unlocked Inventory Projectiles to Inventory 
         AddProjectilesToInventory(ProjectileType.AssaultRifleBullet);
      }  
   }





   #region InitialInventoryProjectileItems
   private void AddProjectilesToInventory(ProjectileType projectileType)
   {
      InventoryItemConstraints inventoryItemConstraints = GetProjectileInventoryItem(projectileType);
      
      InventoryItem item = inventoryItemConstraints.InventoryItem;
      int quantity =  inventoryItemConstraints.InitialQuantity;
      int maxLimit =  inventoryItemConstraints.InitialMaxLimit;
      InventoryService.Instance.AddItemToInventoryList(item,quantity,maxLimit);    
   }

   private InventoryItemConstraints GetProjectileInventoryItem(ProjectileType projectileType)
   {
      InventoryItemConstraints inventoryItemConstraints = null;
      for(int i = 0; i<InitialLevelConstraints.InventoryBulletsLists.Length; i++)
      { 
         if(InitialLevelConstraints.InventoryBulletsLists[i].InventoryItem.InventoryItemType == InventoryItemType.WeaponProjectile)
         {
            WeaponProjectiles temp = (WeaponProjectiles)InitialLevelConstraints.InventoryBulletsLists[i].InventoryItem;

            if(temp.BulletType == projectileType)
            {
               inventoryItemConstraints = InitialLevelConstraints.InventoryBulletsLists[i];
            }
         }
      }
      return inventoryItemConstraints;
   }
   #endregion




   #region InitialMedicalInventoryItems
   private void AddInitialMediKitsToInventory()
   {
       for(int i = 0; i<InitialLevelConstraints.InventoryMedicalKitLists.Length; i++)
       { 
           InventoryItem item = InitialLevelConstraints.InventoryMedicalKitLists[i].InventoryItem;
           int quantity =  InitialLevelConstraints.InventoryMedicalKitLists[i].InitialQuantity;
           int maxLimit =  InitialLevelConstraints.InventoryMedicalKitLists[i].InitialMaxLimit;

           InventoryService.Instance.AddItemToInventoryList(item,quantity,maxLimit);
       }
   }
   #endregion


   public List<CollectiblesScriptableObject> GetProjectileCollectibleList()
   { 
        return InitialLevelConstraints.CollectibleProjectiles;
   }


   public List<CollectiblesScriptableObject> GetMediKitCollectibleList()
   {
        return InitialLevelConstraints.CollectibleHealthKits;
   }

   public void PlayerDied()
   {
      // disable player 
      // enable ui camera 
      // show GameOver Panel
      if(Player.Instance.playerMouseLookController.isCursorLocked())
      {
         Cursor.lockState = CursorLockMode.None;
      }
        
      OnPlayerDeath?.Invoke();
      Player.Instance.playerSoundController.StopPlayerAudios();
      Player.Instance.gameObject.SetActive(false);
      GameOverCamera.SetActive(true);
 
   }

   public void RestartGameButton()
   {  
      GameplayUIManager.Instance.PlayButtonClip();
      Player.Instance.gameObject.transform.position = PlayerRespawnposition.position;
      Player.Instance.playerStatsController.ResetPlayerHealth();
      Player.Instance.playerStatsController.ResetScore();
      Player.Instance.gameObject.SetActive(true);
      GameOverCamera.SetActive(false);
      ResetPlayerInventory();

      Player.Instance.playerSoundController.PlayGamePlaySound();
      OnGameRestart?.Invoke();
   }

   private void ResetPlayerInventory()
   {
      AddInitialMediKitsToInventory();
      AddProjectilesToInventory(ProjectileType.RevolverBullet);
      
      if(GameData.GetShotgunUnlocked() == 1)
      {
         AddProjectilesToInventory(ProjectileType.ShotGunBullet);
      }
      if(GameData.GetAssualtRifleUnlocked() == 1)
      {
         AddProjectilesToInventory(ProjectileType.AssaultRifleBullet);
      }

   }


   public void GameQuitButton()
   {
      // Load Main Menu 
      GameplayUIManager.Instance.PlayButtonClip();
      LevelManager.Instance.LoadScene(0); 
      Time.timeScale = 1;
   }
}
