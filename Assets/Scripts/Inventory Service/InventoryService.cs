using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryService : GenericSingleton<InventoryService>
{
    // 1. it nothing more than bagpack for player 
    // consist one section ( list ) for weapon ammo
    // and one section ( single item (slot) ) for health kits
    // 2. communicate with gameplay ui manager for item updation
    // 3 have event for new InventoryItem Added -> this will add image and quatity to ui slot
    // 4 have event for updatation of quanitty of healthkit and projectile list
    // 3. provide bullet to weapon which are unlocked
    
    public static event Action<ItemSlots> OnNewInventoryItemAdded;
    public static event Action<int> OnProjecileQuantityChanged;
    public static event Action<int> OnHealthKitQuanityChanged; 

    private List<ItemSlots> weaponaryProjectiles;
    
    private ItemSlots HealthKit;


   void Awake()
   {
       base.Awake();
   }
    
   

    // this is called by achievement system only
    public void AddItemSlotToProjectiles(InventoryItem projectile , int Quantity ,int MaxLimit)
    {
       ItemSlots newProjectileItem = new ItemSlots();
       newProjectileItem.SetItem(projectile);
       newProjectileItem.SetQuantity(Quantity);
       newProjectileItem.UpdateMaxLimit(MaxLimit);


       weaponaryProjectiles.Add(newProjectileItem);
       // update the Player Inventory Panel
       OnNewInventoryItemAdded?.Invoke(newProjectileItem);

    }

    // Call only once in lifetime
    public void AddItemToHealthKits(InventoryItem healthKit, int Quantity)
    {
        ItemSlots newHealthKit = new ItemSlots();
        newHealthKit.SetItem(healthKit);
        newHealthKit.SetQuantity(Quantity);

        HealthKit = newHealthKit;

       OnNewInventoryItemAdded?.Invoke(newHealthKit);

    }
     

    // supply bullet to player
    public bool IsBulletAvialable(ProjectileType projectileType)
    {
        for ( int i =0 ; i<weaponaryProjectiles.Count; i++)
        {
            WeaponProjectiles item = (WeaponProjectiles)weaponaryProjectiles[i].InventoryItem;
            if(item.BulletType == projectileType)
            {
                // check quantity if bullet is available or not if yes return true if not return false and generate warning
                if(weaponaryProjectiles[i].GetQuantity() > 0 )
                {
                    // means present 
                    weaponaryProjectiles[i].ReduceQuantity();
                    return true;
                }
                else
                {
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
            MedicalItem item = (MedicalItem)HealthKit.InventoryItem;
            return item.HealthAmount;
        }
       //means health kit is not preset and generate warning msg
       return 0;
    }    



    // add quantity to projectile slots
    public void AddProjectiles(ProjectileType projectileType,int quanitty)
    {
        for ( int i =0 ; i<weaponaryProjectiles.Count; i++)
        {
            WeaponProjectiles item = (WeaponProjectiles)weaponaryProjectiles[i].InventoryItem;
            if(item.BulletType == projectileType)
            {
                // check quantity hits the ma or not if yes return true if not return false and generate warning
                if(weaponaryProjectiles[i].GetQuantity() < weaponaryProjectiles[i].GetMaxQuanity() )
                {
                    // means present 
                    weaponaryProjectiles[i].SetQuantity(quanitty);
                    
                }
                else // means bag is full 
                {
                    //******** ammo full event
                    break;
                }
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
        }
        else
        {
            //******* healtkit full event
        }
    }


}







// UISlot 1 - Arrow InventoryItem
// UISlot 2 - RevBullet InventoryItem 
// UISlot 3 - ShotGunBullet InventoryItem 
// UISlot 4 - AssaultBullet InventoryItem 

// UISlot 5 - FirstAid InventoryItem 