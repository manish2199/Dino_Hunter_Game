using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InventoryService : GenericSingleton<InventoryService>
{
    public static event Action<ProjectileType,int> OnProjectileQuantityChanged;
    public static event Action<HealthKitType,int> OnHealthKitQuanityChanged; 
    
    private List<ItemSlot> weaponaryProjectiles;
    
    private ItemSlot HealthKit;

   // Testing
   public InventoryItem Revbullet;
   public int revquantity;
   public int revmaxLimit; 

 
   

   void Awake()
   {
       base.Awake(); 

       weaponaryProjectiles = new List<ItemSlot>();
   }  


   void Start()
   {
       AddItemSlotToProjectiles(Revbullet,revquantity,revmaxLimit);
   }


  


    // this is called by achievement system only 
    public void AddItemSlotToProjectiles(InventoryItem projectile , int Quantity ,int MaxLimit)
    {
       ItemSlot newProjectileItem = new ItemSlot();

       newProjectileItem.SetItem(projectile);
       newProjectileItem.SetQuantity(Quantity);
       newProjectileItem.UpdateMaxLimit(MaxLimit);

       weaponaryProjectiles.Add(newProjectileItem);
       GameplayUIManager.Instance.AddNewItemToUISlots(newProjectileItem); 
    }

    // Call only once in lifetime
    public void AddItemToHealthKits(InventoryItem healthKit, int Quantity)
    {
        ItemSlot newHealthKit = new ItemSlot();
        newHealthKit.SetItem(healthKit);
        newHealthKit.SetQuantity(Quantity);

        HealthKit = newHealthKit;

       GameplayUIManager.Instance.AddNewItemToUISlots(HealthKit);

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
                    // Debug.Log(weaponaryProjectiles[i].GetQuantity());
                    if(weaponaryProjectiles[i].GetQuantity() <= 5 )
                    {
                        NotificationManager.Instance.ShowNotificationMsg(NotificationType.LowAmmo);
                    }
                    // invoke for text
                    OnProjectileQuantityChanged?.Invoke(projectileType,weaponaryProjectiles[i].GetQuantity());
                    return true;
                }
                else
                {
                     NotificationManager.Instance.ShowNotificationMsg(NotificationType.OutOfAmmo);
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
        if(HealthKit.GetQuantity() > 0)
        {
            // meanse health kit present
            HealthKit.ReduceQuantity();
            OnHealthKitQuanityChanged?.Invoke(healthKitType,HealthKit.GetQuantity());
            MedicalItem item = (MedicalItem)HealthKit.InventoryItem;
            return item.HealthAmount;
        }
       //means health kit is not preset and generate warning msg
       return 0;
        NotificationManager.Instance.ShowNotificationMsg(NotificationType.OutOfHealthKit);
    }


    public void AddItemFromSupplies(CollectibleItemType collectibleItemType , CollectibleItem collectibleItem)
   {
       if(collectibleItemType == CollectibleItemType.Ammunation)
       {
           // add to projectile inventory list
           Bullets temp = (Bullets)collectibleItem;
           AddProjectiles(temp.ProjectileType,temp.CollectibleAmountContain); 
           Debug.Log("Adding to projetiles");
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
                    // means present 
                    
                    weaponaryProjectiles[i].SetQuantity(quanitty);
                    OnProjectileQuantityChanged?.Invoke(projectileType,weaponaryProjectiles[i].GetQuantity()); 
                    print(weaponaryProjectiles[i].GetQuantity());
                }
                else // means bag is full 
                {
                    //******** ammo full event
                    NotificationManager.Instance.ShowNotificationMsg(NotificationType.FullAmmo);
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
       if( HealthKit.GetQuantity() < HealthKit.GetMaxQuanity() )
        {
            // means health comp can store more
            HealthKit.SetQuantity(Quantity);
            OnHealthKitQuanityChanged?.Invoke(healthKitType,HealthKit.GetQuantity());
        }
        else
        {
             NotificationManager.Instance.ShowNotificationMsg(NotificationType.FullHealthKit);
            //******* healtkit full event
        }
    }


}
