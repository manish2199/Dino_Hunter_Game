using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewWeaponScriptableObject")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] WeaponType weaponType;
    [SerializeReference] Weapon weapon;

    private WeaponType lastType;
    private Weapon savedWeapon;

    public WeaponType WeaponType => weaponType;
    public Weapon Weapon { get { return weapon; } }  
    
    private bool IsUnityLoaded = false; 

    [HideInInspector,SerializeReference] public ShootableWeapons ShootableWeapons;
    // [HideInInspector,SerializeReference] public NonShootableWeapons NonShootableWeapons;


    public void Awake()
    {
        lastType = WeaponType;  
        IsUnityLoaded = true;
    }


    public WeaponScriptableObject()
    {
        lastType = WeaponType;
    }

    void OnValidate()
    {
        if(weaponType != lastType && IsUnityLoaded)
        { 
            lastType = weaponType;
            savedWeapon = weapon;
            weapon = UpdateWeaponType(savedWeapon);
            savedWeapon = null;
        }
    }

    private Weapon UpdateWeaponType(Weapon other)
    {
        Weapon newWeapon = null;
        
        switch(weaponType)
        {
             case WeaponType.Shootable : 
                newWeapon = new ShootableWeapons();
                ShootableWeapons = (ShootableWeapons)newWeapon;
                break;

             case WeaponType.NonShootable : 
                newWeapon = new NonShootableWeapons();
                break;
        }
        return newWeapon;
    }
}

