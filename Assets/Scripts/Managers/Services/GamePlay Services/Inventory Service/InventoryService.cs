using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryService : GenericSingleton<InventoryService>
{

    public static event Action<InventoryItem,int>OnItemQuantityChanged;
    public static event Action<InventoryItem,int>OnItemMaxLimitChanged;
  
    private List<ItemSlot> InventoryItemList;

    public static event Action<NotificationType> OnItemCollected;
    
    protected override void Awake()
   {
       base.Awake();
        
       InventoryItemList = new List<ItemSlot>();
   }  

   private void MakeInstance()
   {
       base.Awake();
   }

    public void AddItemToInventoryList(InventoryItem item , int Quantity , int  MaxLimit)
    {
        ItemSlot newItem = new ItemSlot();

        newItem.SetItem(item);
        newItem.UpdateMaxLimit(MaxLimit);
        
        newItem.SetQuantity(Quantity);
        InventoryItemList.Add(newItem);
        GameplayUIManager.Instance.AddNewItemToUISlots(newItem);
    }
     

    // supply bullet to player
    public bool IsBulletAvialable(ProjectileType projectileType)
    {

        for ( int i =0 ; i<InventoryItemList.Count; i++)
        {
          if( InventoryItemList[i].InventoryItem.InventoryItemType == InventoryItemType.WeaponProjectile)
          {
            WeaponProjectiles item = (WeaponProjectiles)InventoryItemList[i].InventoryItem;
            if(item.BulletType == projectileType)
            {
                if(InventoryItemList[i].GetQuantity() > 0 )
                {
                    // means present 
                    InventoryItemList[i].ReduceQuantity();  
                    if(InventoryItemList[i].GetQuantity() <= 5 )
                    {    
                        if(NotificationManager.Instance != null)
                        {
                          NotificationManager.Instance.ShowNotificationMsg(NotificationType.LowAmmo);
                        }
                    }
                    // invoke for text
                    OnItemQuantityChanged?.Invoke(InventoryItemList[i].InventoryItem,InventoryItemList[i].GetQuantity());
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
        }
        // Show empty ammo warning msg
        return false;
    }


    

    // supply health kit to player
    public int GetHealthKit( HealthKitType healthKitType )
    {
        for ( int i =0 ; i<InventoryItemList.Count; i++)
        {
          if( InventoryItemList[i].InventoryItem.InventoryItemType == InventoryItemType.HealthKit)
          {
            MedicalItem item = (MedicalItem)InventoryItemList[i].InventoryItem;
            if(item.HealthKitType == healthKitType)
            {
                if(InventoryItemList[i].GetQuantity() > 0 )
                {
                    // means present 
                    InventoryItemList[i].ReduceQuantity();  
                    
                    // invoke for text
                    OnItemQuantityChanged?.Invoke(InventoryItemList[i].InventoryItem,InventoryItemList[i].GetQuantity());
                    return item.HealthAmount;
                }
                else
                {
                    break;
                }
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
        for ( int i =0 ; i<InventoryItemList.Count; i++)
        {
          if( InventoryItemList[i].InventoryItem.InventoryItemType == InventoryItemType.WeaponProjectile)
          { 
            WeaponProjectiles item = (WeaponProjectiles)InventoryItemList[i].InventoryItem;
            if(item.BulletType == projectileType)
            {
                // check quantity hits the max or not if yes return true if not return false and generate warning
                if(InventoryItemList[i].GetQuantity() < InventoryItemList[i].GetMaxQuanity() )
                {
                    // means space is availabe
                    OnItemCollected?.Invoke(NotificationType.ItemCollectedNotification); 
                    InventoryItemList[i].SetQuantity(quanitty); 
                    OnItemQuantityChanged?.Invoke(InventoryItemList[i].InventoryItem,InventoryItemList[i].GetQuantity());
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
            // means not found 
          }
        }

    }



    // add quantity to healthkit slot  
    public void AddHealthKits( HealthKitType healthKitType ,int Quantity)
    {
        for ( int i =0 ; i<InventoryItemList.Count; i++)
        {
          if( InventoryItemList[i].InventoryItem.InventoryItemType == InventoryItemType.HealthKit)
          {
            MedicalItem item = (MedicalItem)InventoryItemList[i].InventoryItem;
            if(item.HealthKitType == healthKitType)
            {
                // check quantity hits the max or not if yes return true if not return false and generate warning
                if(InventoryItemList[i].GetQuantity() < InventoryItemList[i].GetMaxQuanity() )
                {
                    // means present 
                    OnItemCollected?.Invoke(NotificationType.ItemCollectedNotification);
                    InventoryItemList[i].SetQuantity(Quantity);
                    OnItemQuantityChanged?.Invoke(InventoryItemList[i].InventoryItem,InventoryItemList[i].GetQuantity());

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
    }


    public void IncreaseHealthKitsMaxLimit(int maxLimit,HealthKitType healthKitType)
    {
        for(int i = 0; i<InventoryItemList.Count; i++)
        {
          if( InventoryItemList[i].InventoryItem.InventoryItemType == InventoryItemType.HealthKit)
          {
            MedicalItem item = (MedicalItem)InventoryItemList[i].InventoryItem;
            
            if(item.HealthKitType == healthKitType)
            {
              InventoryItemList[i].UpdateMaxLimit(maxLimit);
              if(GameplayUIManager.Instance != null)
              { 
                 OnItemMaxLimitChanged?.Invoke(InventoryItemList[i].InventoryItem,InventoryItemList[i].GetMaxQuanity());
              }
            } 
          }
        }
    }
 
}
