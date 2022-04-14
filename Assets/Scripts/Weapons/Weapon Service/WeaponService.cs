using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponService : GenericSingleton<WeaponService>
{
    // Initially Unlock Only Two Weapons
    // As Player achieve something then achievement system unlocks Weapon ( add weapons in list ) (hint - use events)
    
    [SerializeField] private WaponScriptableObjectList weapons;

    [SerializeReference] List<WeaponController> weaponControllerList;
    private int currentSelectedWeapon;

    public static event Action<bool> OnWeaponZoomIn;
  
   protected override void Awake()
   {
        base.Awake();
        weaponControllerList = new List<WeaponController>();
        UnlockTheWeapon(WeaponsID.Axe); 
        UnlockTheWeapon(WeaponsID.Revolver); 
        // UnlockTheWeapon(WeaponsID.ShotGun); 
        // UnlockTheWeapon(WeaponsID.AssaultRifle);  
   }
   
    
    public void InvokeOnZoomIn(bool isZoomed)
    {
        OnWeaponZoomIn?.Invoke(isZoomed);
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

        WeaponController weaponController = null;
    
        if(weaponScriptableObject.WeaponType == WeaponType.Shootable)
        {
             ShootableWeaponModel shootableWeaponModel = new ShootableWeaponModel(weaponScriptableObject);

             weaponController = new ShootableWeaponController (shootableWeaponModel,weaponScriptableObject.Weapon.weaponView);
        }
        else
        {
            NonShootableWeaponModel nonShootableWeaponModel = new NonShootableWeaponModel(weaponScriptableObject);

            weaponController = new NonShootableWeaponController( nonShootableWeaponModel ,weaponScriptableObject.Weapon.weaponView);  
        }

        return weaponController;   
    }


    public void SelectWeapon(Transform fpsTransform,int index)
    {
        // check if weapon is present or not in list , if not then return and show weapon slot is empty 
        // if present then deactivate current activated weapon and activate selected weapon
           
           
        if(index == currentSelectedWeapon || index >= weaponControllerList.Count)
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