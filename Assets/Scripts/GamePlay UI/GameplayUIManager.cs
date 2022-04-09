using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : GenericSingleton<GameplayUIManager>
{
    // Listens events for new slot added
    // Listens events for item quantity updation


    [SerializeField] ProjectileInventoryUISlot[] projectileInventoryUISlots;

    [SerializeField] HealthKitInventoryUISlot[] healthKitInventoryUISlot;


    [SerializeField] GameObject CrossHair;

    
    void OnEnable()
    {
       InventoryService.OnNewInventoryItemAdded += AddNewItemToUISlots;
       WeaponService.OnWeaponZoomIn += SetCrossHair;
    }

    void OnDisable()
    {
       InventoryService.OnNewInventoryItemAdded -= AddNewItemToUISlots;
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


    void AddNewItemToUISlots(ItemSlots itemSlot)
    {
       if( itemSlot.InventoryItem.InventoryItemType == InventoryItemType.WeaponProjecile)
       {
           // add inside projectiles slot;
       }
       else if( itemSlot.InventoryItem.InventoryItemType == InventoryItemType.HealthKit  )
       {
           // add inside healthKit Slot
       }
    }
   
}



[System.Serializable]
public class ProjectileInventoryUISlot
{
    public Text ItemQuantityText;
    public Sprite ItemIcon;
    [HideInInspector]public bool isEmpty;       
    [HideInInspector] public ProjectileType projectileType;
    
    ProjectileInventoryUISlot()
    {
        isEmpty = true;
    }
}


[System.Serializable]
public class HealthKitInventoryUISlot
{
    public Text ItemQuantityText;
    public Sprite ItemIcon;
    [HideInInspector]public bool isEmpty;       
    [HideInInspector] public HealthKitType healthKitType;
    
    HealthKitInventoryUISlot()
    {
        isEmpty = true;
    }
}
