using System;
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
     
    void Awake()
    { 
        base.Awake();
       
       for(int i=0; i<projectileInventoryUISlots.Length; i++)
       {
           projectileInventoryUISlots[i].isEmpty = true;
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
       if( tempItem.InventoryItemType == InventoryItemType.WeaponProjecile)
       {
        //    add inside projectiles slot which are empty;
           if(projectileInventoryUISlots.Length > 0)
           { 
                for ( int i = 0; i< projectileInventoryUISlots.Length; i++)
                { 
                    print(projectileInventoryUISlots[i].isEmpty);
                    if( projectileInventoryUISlots[i].isEmpty)
                    { 
                    //    WeaponProjectiles temp =(WeaponProjectiles)itemSlot.InventoryItem;
                        // initial setup
                        
                        projectileInventoryUISlots[i].EmptyItemText.gameObject.SetActive(false);
                        projectileInventoryUISlots[i].IconGameObject.SetActive(true);
                        projectileInventoryUISlots[i].ItemQuantityText.gameObject.SetActive(true);

                        projectileInventoryUISlots[i].IconGameObject.GetComponent<Image>().sprite  = itemSlot.InventoryItem.UIIcon;
                        projectileInventoryUISlots[i].ItemQuantityText.text = itemSlot.GetQuantity().ToString(); 
                        projectileInventoryUISlots[i].isEmpty = false;
                        WeaponProjectiles temp  = (WeaponProjectiles)tempItem;
                        projectileInventoryUISlots[i].ProjectileType  = temp.BulletType;
                        Debug.Log(temp.BulletType); 
                        print( projectileInventoryUISlots[i].ProjectileType);
                        break; 
                    }
                }
           }
       }
       else if( tempItem.InventoryItemType == InventoryItemType.HealthKit  )
       {
        //    add inside healthKit Slot
       }
    }


    public void UpdateTheProjectilesQuantity(ProjectileType projectileType , int quanitty) 
    {
        for ( int i =0 ; i<projectileInventoryUISlots.Length; i++)
        {
            print("Updating UI");
            if(projectileType == projectileInventoryUISlots[i].ProjectileType)
            {
                projectileInventoryUISlots[i].ItemQuantityText.text = quanitty.ToString();
            }

        }

    }

    public void UpdateMedicalKitQuantity( HealthKitType healthKitType ,int quanitty) 
    {
       for ( int i =0 ; i<healthKitInventoryUISlot.Length; i++)
        {
            if(healthKitType == healthKitInventoryUISlot[i].healthKitType)
            {
                healthKitInventoryUISlot[i].ItemQuantityText.text = quanitty.ToString();
            }

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
   
}



[Serializable]
public class ProjectileInventoryUISlot
{
    public Text EmptyItemText;
    public Text ItemQuantityText;
    public GameObject IconGameObject; 
    [HideInInspector]public bool isEmpty;       
    [HideInInspector] public ProjectileType  ProjectileType;
}


[System.Serializable]
public class HealthKitInventoryUISlot
{
    public Text EmptyItemText;
    public Text ItemQuantityText;
    public GameObject IconGameObject; 
    [HideInInspector]public bool isEmpty;       
    [HideInInspector] public HealthKitType healthKitType;
    
    // HealthKitInventoryUISlot()
    // {
        // isEmpty = true;
    // }
}
