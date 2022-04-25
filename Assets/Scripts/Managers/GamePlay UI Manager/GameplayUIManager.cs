using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class GameplayUIManager : GenericSingleton<GameplayUIManager>
{
    // Listens events for new slot added
    // Listens events for item quantity updation 

    [SerializeField] AudioSource ButtonAudioSource;

    [SerializeField] GameObject InventoryUIPanel;

    [SerializeField] ProjectileInventoryUISlot[] projectileInventoryUISlots;

    [SerializeField] HealthKitInventoryUISlot[] healthKitInventoryUISlot;

    [SerializeField] GameObject ScorePanel;
     [SerializeField] TextMeshProUGUI PlayerScoreText; 

    [SerializeField] GameObject CrossHair; 

    [SerializeField] Image PlayerDamageIndicator;

    [SerializeField] Image CurrentSelectedWeaponIcon;

    // Game Over
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] TextMeshProUGUI DifficultyLevelText;
    [SerializeField] TextMeshProUGUI GameOverScoreText;
    [SerializeField] TextMeshProUGUI HighScoreText; 
    [SerializeField] GameObject MiniMapPanel;
    
  
    // Pasue Menu 
    [SerializeField] GameObject PauseMenuPanel;
    [SerializeField] Button PauseButton;
    [SerializeField] Button ResumeButton;
    [SerializeField] Button ControlsButton;
    [SerializeField] Button QuitButton;
    [SerializeField] TextMeshProUGUI PauseText;
    [SerializeField] GameObject ControlsPanel;
 
 

     
   protected override void Awake()
    { 
      if(Instance == null)
      {
         Instance = this;
      }
       
       for(int i=0; i<projectileInventoryUISlots.Length; i++)
       {
           projectileInventoryUISlots[i].isEmpty = true;
       }

        for(int i=0; i<healthKitInventoryUISlot.Length; i++)
       {
           healthKitInventoryUISlot[i].isEmpty = true;
       }
    }
    
    void OnEnable()
    {
       InventoryService.OnProjectileQuantityChanged += UpdateTheProjectilesQuantity;
       InventoryService.OnHealthKitQuanityChanged += UpdateMedicalKitQuantity;
       WeaponService.OnWeaponZoomIn += SetCrossHair; 
       GamePlayManager.OnPlayerDeath += DisableUIElements;
       GamePlayManager.OnGameRestart += EnableUIElements;
    }

    void OnDisable()
    {
       InventoryService.OnProjectileQuantityChanged -= UpdateTheProjectilesQuantity;
       InventoryService.OnHealthKitQuanityChanged += UpdateMedicalKitQuantity;
       WeaponService.OnWeaponZoomIn -= SetCrossHair;
       GamePlayManager.OnPlayerDeath -= DisableUIElements;
       GamePlayManager.OnGameRestart += EnableUIElements;
    }

    public void SetCrossHair(bool isWeaponZoomed)
    {
        if(isWeaponZoomed)
        {
            CrossHair.SetActive(false);
        }
        else
        {
            CrossHair.SetActive(true);
        }
    }


    public void AddNewItemToUISlots(ItemSlot itemSlot)
    {
       InventoryItem tempItem = itemSlot.InventoryItem;
       if( tempItem.InventoryItemType == InventoryItemType.WeaponProjectile)
       {
        //    add inside projectiles slot which are empty;
           if(projectileInventoryUISlots.Length > 0)
           { 
                for ( int i = 0; i< projectileInventoryUISlots.Length; i++)
                { 
                    // print(projectileInventoryUISlots[i].isEmpty);
                    if( projectileInventoryUISlots[i].isEmpty)
                    {  
                        // Initial sETup
                        InitialProjectileSetup(projectileInventoryUISlots[i],itemSlot,tempItem);
                        break; 
                    }
                }
           }
       }
       else if( tempItem.InventoryItemType == InventoryItemType.HealthKit  )
       {
        //    add inside healthKit Slot
            if(healthKitInventoryUISlot.Length > 0)
           { 
                for ( int i = 0; i< healthKitInventoryUISlot.Length; i++)
                {
                    if( healthKitInventoryUISlot[i].isEmpty)
                    {  
                        // Initial sETup
                        InitialMedicalItemSetup(healthKitInventoryUISlot[i],itemSlot,tempItem);
                        break; 
                    }
                }
           }
       } 
    }


    private void InitialProjectileSetup(ProjectileInventoryUISlot projectile,ItemSlot itemSlot, InventoryItem tempItem)
    {
        projectile.EmptyItemText.gameObject.SetActive(false);
        projectile.IconGameObject.SetActive(true);
        projectile.ItemQuantityText.gameObject.SetActive(true);
        projectile.MaxLimit.gameObject.SetActive(true);
        projectile.ItemQuantityText.gameObject.SetActive(true);

        projectile.IconGameObject.GetComponent<Image>().sprite  = itemSlot.InventoryItem.UIIcon;
        projectile.ItemQuantityText.text = itemSlot.GetQuantity().ToString(); 
        projectile.ItemMaxLimitText.text = itemSlot.GetMaxQuanity().ToString();
        projectile.isEmpty = false;
        WeaponProjectiles temp  = (WeaponProjectiles)tempItem;
        projectile.ProjectileType  = temp.BulletType;
    }


    private void InitialMedicalItemSetup(HealthKitInventoryUISlot healthKit,ItemSlot itemSlot, InventoryItem tempItem)
    {
        healthKit.EmptyItemText.gameObject.SetActive(false);
        healthKit.IconGameObject.SetActive(true);
        healthKit.ItemQuantityText.gameObject.SetActive(true);
        healthKit.MaxLimit.gameObject.SetActive(true);
        healthKit.ItemQuantityText.gameObject.SetActive(true);

        healthKit.IconGameObject.GetComponent<Image>().sprite  = itemSlot.InventoryItem.UIIcon;
        healthKit.ItemMaxLimitText.text = itemSlot.GetMaxQuanity().ToString();
        healthKit.ItemQuantityText.text = itemSlot.GetQuantity().ToString(); 
        healthKit.isEmpty = false;
        MedicalItem temp  = (MedicalItem)tempItem;
        healthKit.HealthKitType  = temp.HealthKitType;
    }

    public void UpdatePlayerScore(int Score)
    {
       PlayerScoreText.text = Score.ToString();
    }

    public void UpdateTheProjectilesQuantity(ProjectileType projectileType , int quanitty) 
    {
        for ( int i =0 ; i<projectileInventoryUISlots.Length; i++)
        {
            if(projectileType == projectileInventoryUISlots[i].ProjectileType)
            {
                projectileInventoryUISlots[i].ItemQuantityText.text = quanitty.ToString();
                break;
            }

        }

    }

    public void UpdateTheProjectilesMaxLimit(ProjectileType projectileType , int maxLimit) 
    {
        for ( int i =0 ; i<projectileInventoryUISlots.Length; i++)
        {
            if(projectileType == projectileInventoryUISlots[i].ProjectileType)
            {
                projectileInventoryUISlots[i].ItemMaxLimitText.text = maxLimit.ToString();
            }

        }

    }

    public void UpdateMedicalKitQuantity( HealthKitType healthKitType ,int quanitty) 
    {
       for ( int i =0 ; i<healthKitInventoryUISlot.Length; i++)
        {
            if(healthKitType == healthKitInventoryUISlot[i].HealthKitType)
            {
                healthKitInventoryUISlot[i].ItemQuantityText.text = quanitty.ToString(); 
                break;
            }

        }
    }

    public void UpdateHealthKitMaxLimit(int maxLimit)
    {
        for ( int i =0 ; i<healthKitInventoryUISlot.Length; i++)
        {
            healthKitInventoryUISlot[i].ItemMaxLimitText.text = maxLimit.ToString();
        }
    }

    public void ActivateInventory()
    {
        if(InventoryUIPanel.activeInHierarchy)
        {
           InventoryUIPanel.SetActive(false);
        }
        else
        {
           InventoryUIPanel.SetActive(true);
        }
    }

    public void UpdateDamageIndicator(float damageDealed) 
    {
        float DamageIndicatorAlphaValue = damageDealed / 100 ;
         
        var tempColor = PlayerDamageIndicator.color;
        tempColor.a = DamageIndicatorAlphaValue;
        PlayerDamageIndicator.color = tempColor;
    } 

    public void UpdateSelectedWeaponIcon(Sprite weaponIcon)
    { 
       CurrentSelectedWeaponIcon.sprite = weaponIcon; 
    }

    public void EnableSelectedWeaponIcon()
    {
        CurrentSelectedWeaponIcon.gameObject.SetActive(true);
    }   

    public void DisableSelectedWeaponIcon()
    {
        CurrentSelectedWeaponIcon.gameObject.SetActive(false);
    }



    public void ShowGameOverUIPanel()
    {
        if(GameData.GetEasyDifficulty() == 1)
       {
          DifficultyLevelText.text = "Easy";
          HighScoreText.text = GameData.GetEasyDifficultyHighScore().ToString(); 
       }
       if(GameData.GetMediumDifficulty() == 1)
       {
          DifficultyLevelText.text = "Medium";
          HighScoreText.text = GameData.GetMediumDifficultyHighScore().ToString(); 
       }
       if(GameData.GetHardDifficulty() == 1)
       {
          DifficultyLevelText.text = "Hard";
          HighScoreText.text = GameData.GetHardDifficultyHighScore().ToString(); 
       }
       
       GameOverScoreText.text = PlayerScoreText.text; 

       GameOverPanel.SetActive(true);
    }

    public void DisableGameOverUIPanel()
    {
       GameOverPanel.SetActive(false);
    }

    public void EnableMiniMap()
    {
        MiniMapPanel.SetActive(true);
    }

    public void DisableMiniMap()
    {
        MiniMapPanel.SetActive(false);
    }


    public void PauseGame()
    {
        PlayButtonClip();
        SetCrossHair(true);
        DisableMiniMap();
        PauseMenuPanel.SetActive(true);
        ScorePanel.SetActive(false);
        CurrentSelectedWeaponIcon.gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        PlayButtonClip();
        Time.timeScale = 1;
        SetCrossHair(false);
        EnableMiniMap();
        PauseMenuPanel.SetActive(false);
        ScorePanel.SetActive(true);
        CurrentSelectedWeaponIcon.gameObject.SetActive(true);
        PauseButton.gameObject.SetActive(true);
    }

    public void OpenControls()
    {  
        PlayButtonClip();
        PauseText.gameObject.SetActive(false);
        ResumeButton.gameObject.SetActive(false);
        ControlsButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
        ControlsPanel.SetActive(true);
    }

    public void ControlsExit()
    { 
        PlayButtonClip();
        PauseText.gameObject.SetActive(true);
        ResumeButton.gameObject.SetActive(true);
        ControlsButton.gameObject.SetActive(true);
        QuitButton.gameObject.SetActive(true);
        ControlsPanel.SetActive(false);
    }

    private void DisableUIElements()
    {
       DisableMiniMap();
       DisableSelectedWeaponIcon();
       SetCrossHair(true);
       ShowGameOverUIPanel();
    }


    private void EnableUIElements()
    { 
      EnableSelectedWeaponIcon();
      SetCrossHair(false);
      DisableGameOverUIPanel();
      EnableMiniMap();
    }

    public void PlayButtonClip()
    {
        ButtonAudioSource.Play();
    }
}






[Serializable]
public class InventoryUISlot
{
    public Text EmptyItemText;
    public Text ItemQuantityText;
    public Text MaxLimit; 
    public Text ItemMaxLimitText;
    public GameObject IconGameObject;  
    [HideInInspector]public bool isEmpty;       
}


[Serializable]
public class ProjectileInventoryUISlot : InventoryUISlot
{  
    [HideInInspector] public ProjectileType  ProjectileType;
}


[System.Serializable]
public class HealthKitInventoryUISlot : InventoryUISlot
{
    [HideInInspector] public HealthKitType HealthKitType;
}
