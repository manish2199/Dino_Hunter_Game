﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : GenericSingleton<GameplayUIManager>
{
    // Listens events for new slot added
    // Listens events for item quantity updation

    [SerializeField] GameObject InventoryUIPanel;

    [SerializeField] ProjectileInventoryUISlot[] projectileInventoryUISlots;

    [SerializeField] HealthKitInventoryUISlot[] healthKitInventoryUISlot;

    [SerializeField] GameObject CrossHair; 

    [SerializeField] Image PlayerDamageIndicator;
     
    void Awake()
    { 
        base.Awake();
       
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
    }

    void OnDisable()
    {
       InventoryService.OnProjectileQuantityChanged -= UpdateTheProjectilesQuantity;
       InventoryService.OnHealthKitQuanityChanged += UpdateMedicalKitQuantity;
       WeaponService.OnWeaponZoomIn -= SetCrossHair;
    }

    private void SetCrossHair(bool isWeaponZoomed)
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
        projectile.EmptyItemText.SetActive(false);
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
        healthKit.EmptyItemText.SetActive(false);
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
         
        // image = GetComponent<Image>();
          var tempColor = PlayerDamageIndicator.color;
          tempColor.a = DamageIndicatorAlphaValue;
          PlayerDamageIndicator.color = tempColor;
    } 
   
}


[Serializable]
public class InventoryUISlot
{
    public GameObject EmptyItemText;
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
