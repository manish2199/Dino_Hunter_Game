﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponService : GenericSingleton<WeaponService>
{
    // Initially Unlock Only Two Weapons
    // As Player achieve something then achievement system unlocks Weapon ( add weapons in list ) (hint - use events)
    
   [SerializeField] private WaponScriptableObjectList weapons;


    private WeaponController[] weaponsController;
    List<WeaponController> weaponControllerList;
    private int currentSelectedWeapon;

  
   protected override void Awake()
   {
        // MakeSingleton();
        base.Awake();
        weaponControllerList = new List<WeaponController>();
        UnlockTheWeapon(WeaponsID.Axe); 
        UnlockTheWeapon(WeaponsID.Bow); 
   }


    void Start()
    {
    }  


    public void UnlockTheWeapon(WeaponsID weaponsID)
    {
        //  get weapon scriptableobject from lists
        // create weapon controller 
        // add weapon controller inside controller list

        WeaponScriptableObject weapon = weapons.GetWeapon(weaponsID);

        WeaponController weaponController = CreateNewWeaponController(weapon);
    
        AddWeaponControllerInList(weaponController); 
    }

    private void AddWeaponControllerInList(WeaponController weaponController)
    {
        weaponControllerList.Add(weaponController);
    }


    private WeaponController CreateNewWeaponController(WeaponScriptableObject weaponScriptableObject)
    {
        // Create new model using scriptable object 
        // initialie new weapon controller using model and view
        // return weapon controller
    
        WeaponModel weaponModel = new WeaponModel(weaponScriptableObject);

        WeaponController weaponController = new WeaponController (weaponModel,weaponScriptableObject.Weapon.weaponView);

        return weaponController;   
    }


    public void SelectWeapon(Transform fpsTransform,int index)
    {
        // check if weapon is present or not in list , if not then return and show weapon slot is empty 
        // if present then deactivate current activated weapon and activate selected weapon
        
        if(index > weaponControllerList.Count)
        {
            // weapon is not unlocked
            return;
        }

        if(index == currentSelectedWeapon)
        {
            return;
        }

        weaponControllerList[currentSelectedWeapon].DeactivateWeapon();

        weaponControllerList[index].ActivateWeapon(fpsTransform);

        currentSelectedWeapon = index;

    }

    public void SelectInitialWeapon(Transform fpsTransform)
    {
       currentSelectedWeapon = 0;
       weaponControllerList[0].ActivateWeapon(fpsTransform); 
    }
    
}




// To find element inside list
  // PoolItem<T> item = pooledItems.Find( I => I.isUsed == false);