﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryService : GenericSingleton<InventoryService>
{
    public static event Action<ProjectileType,int> OnProjectileQuantityChanged;
    public static event Action<HealthKitType,int> OnHealthKitQuanityChanged; 
    
    private List<ItemSlot> weaponaryProjectiles;
    
    private List<ItemSlot> HealthKits;  

    public static event Action<NotificationType> OnItemCollected;
    
    protected override void Awake()
   {
       MakeInstance();
        
       HealthKits = new List<ItemSlot>();
       weaponaryProjectiles = new List<ItemSlot>();
   }  

   private void MakeInstance()
   {
       if(Instance == null)
       {
           Instance = this;
       }
   }


    // this is called by achievement system only 
    public void AddItemSlotToProjectiles(InventoryItem projectile , int Quantity ,int MaxLimit)
    {
       ItemSlot newProjectileItem = new ItemSlot();

       newProjectileItem.SetItem(projectile);
       newProjectileItem.SetQuantity(Quantity);
       newProjectileItem.UpdateMaxLimit(MaxLimit);

       weaponaryProjectiles.Add(newProjectileItem);
       
    //    OnNewItemAdded?.Invoke(newProjectileItem)
        GameplayUIManager.Instance.AddNewItemToUISlots(newProjectileItem); 
    }

    // Call only once in lifetime
    public void AddItemToHealthKits(InventoryItem healthKit, int Quantity , int MaxLimit )
    {
        ItemSlot newHealthKit = new ItemSlot();

        newHealthKit.SetItem(healthKit);
        newHealthKit.SetQuantity(Quantity);
        newHealthKit.UpdateMaxLimit(MaxLimit);
        
            //    Debug.Log("Adding HealthKit"); 
        HealthKits.Add(newHealthKit);
        GameplayUIManager.Instance.AddNewItemToUISlots(newHealthKit);
    }
     

    // supply bullet to player
    public bool IsBulletAvialable(ProjectileType projectileType)
    {

        for ( int i =0 ; i<weaponaryProjectiles.Count; i++)
        {
            WeaponProjectiles item = (WeaponProjectiles)weaponaryProjectiles[i].InventoryItem;
            if(item.BulletType == projectileType)
            {
                if(weaponaryProjectiles[i].GetQuantity() > 0 )
                {
                    // means present 
                    weaponaryProjectiles[i].ReduceQuantity();  
                    if(weaponaryProjectiles[i].GetQuantity() <= 5 )
                    {    
                        if(NotificationManager.Instance != null)
                        {
                          NotificationManager.Instance.ShowNotificationMsg(NotificationType.LowAmmo);
                        }
                    }
                    // invoke for text
                    OnProjectileQuantityChanged?.Invoke(projectileType,weaponaryProjectiles[i].GetQuantity());
                    return true;
                }
                else
                {  
                    if(NotificationManager.Instance != null)
                    {  
                    NotificationManager.Instance.ShowNotificationMsg(NotificationType.OutOfAmmo);
                    }
                    break;
                }
            }
        }
        // Show empty ammo warning msg
        return false;
    }

    // supply health kit to player
    public int GetHealthKit( HealthKitType healthKitType )
    {
        for ( int i =0 ; i<HealthKits.Count; i++)
        {
            MedicalItem item = (MedicalItem)HealthKits[i].InventoryItem;
            if(item.HealthKitType == healthKitType)
            {
                if(HealthKits[i].GetQuantity() > 0 )
                {
                    // means present 
                    HealthKits[i].ReduceQuantity(); 
                //    Debug.Log("Using HealthKit");  
                    
                    // invoke for text
                    OnHealthKitQuanityChanged?.Invoke(healthKitType,HealthKits[i].GetQuantity());
                    return item.HealthAmount;
                }
                else
                {
                    break;
                }
            }
        }
       //means health kit is not preset and generate warning msg
        return 0;
        if(NotificationManager.Instance != null)
       {
          NotificationManager.Instance.ShowNotificationMsg(NotificationType.OutOfHealthKit);
       }
    }


    public void AddItemFromSupplies(CollectibleItemType collectibleItemType , CollectibleItem collectibleItem)
   {
       if(collectibleItemType == CollectibleItemType.Ammunation)
       {
           // add to projectile inventory list
           Bullets temp = (Bullets)collectibleItem;
           AddProjectiles(temp.ProjectileType,temp.CollectibleAmountContain); 
       }
       if(collectibleItemType == CollectibleItemType.Medical)
       {
           // add to medical inventory list
            HealthKit temp = (HealthKit)collectibleItem;
           AddHealthKits(temp.HealthKitType,temp.CollectibleAmountContain);
       }
   }    



    // add quantity to projectile slots from supplies
    public void AddProjectiles(ProjectileType projectileType,int quanitty)
    {
        for ( int i =0 ; i<weaponaryProjectiles.Count; i++)
        {
            WeaponProjectiles item = (WeaponProjectiles)weaponaryProjectiles[i].InventoryItem;
            if(item.BulletType == projectileType)
            {
                // check quantity hits the max or not if yes return true if not return false and generate warning
                if(weaponaryProjectiles[i].GetQuantity() < weaponaryProjectiles[i].GetMaxQuanity() )
                {
                    // means space is availabe
                    // NotificationManager.Instance.ShowNotificationMsg(NotificationType.ItemCollectedNotification);
                    OnItemCollected?.Invoke(NotificationType.ItemCollectedNotification);
                    weaponaryProjectiles[i].SetQuantity(quanitty);
                    OnProjectileQuantityChanged?.Invoke(projectileType,weaponaryProjectiles[i].GetQuantity()); 
                    
                }
                else // means bag is full 
                {
                    //******** ammo full event
                    if(NotificationManager.Instance != null)
                    {
                       NotificationManager.Instance.ShowNotificationMsg(NotificationType.FullAmmo);
                    }
                    break;
                }
            } 
            else
            {
                // means not found 
            }
        }

    }



    // add quantity to healthkit slot  
    public void AddHealthKits( HealthKitType healthKitType ,int Quantity)
    {
        for ( int i =0 ; i<HealthKits.Count; i++)
        {
            MedicalItem item = (MedicalItem)HealthKits[i].InventoryItem;
            if(item.HealthKitType == healthKitType)
            {
                // check quantity hits the max or not if yes return true if not return false and generate warning
                if(HealthKits[i].GetQuantity() < HealthKits[i].GetMaxQuanity() )
                {
                    // means present 
                    OnItemCollected?.Invoke(NotificationType.ItemCollectedNotification);
                    HealthKits[i].SetQuantity(Quantity);
                    OnHealthKitQuanityChanged?.Invoke(healthKitType,HealthKits[i].GetQuantity());
                }
                else // means bag is full 
                {
                    if(NotificationManager.Instance != null)
                    {
                      NotificationManager.Instance.ShowNotificationMsg(NotificationType.FullHealthKit);
                    }
                    break;
                }
            } 
            else
            {
                // means not found 
            }
        }
    }


    public void IncreaseHealthKitsMaxLimit(int maxLimit)
    {
        for(int i = 0; i<HealthKits.Count; i++)
        {
            HealthKits[i].UpdateMaxLimit(maxLimit);
            if(GameplayUIManager.Instance != null)
            { 
              GameplayUIManager.Instance.UpdateHealthKitMaxLimit(maxLimit);
            }
        }
    }

    void OnDestroy()
    {
        HealthKits= null;
    }
 

}
