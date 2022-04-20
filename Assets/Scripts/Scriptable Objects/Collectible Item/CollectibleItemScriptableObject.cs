using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewCollectibleScriptableObjectList")]
public class CollectiblesScriptableObject : ScriptableObject
{
    // Collectible revolverBulletsCollecetible =  new Collectible();
    // revolverBulletsCollecetible.CollectibleItemType = CollectibleItemType.Ammunation;
    // Bullets revBullet = new Bullets();
    // revBullet.ProjectileType = ProjectileType.RevolverBullet;
    // revBullet.CollectibleAmountContain = 20;
    // revolverBulletsCollecetible.CollectibleItem = revBullet;

    [SerializeField] CollectibleItemType collectibleItemType;
    [SerializeReference] CollectibleItem collectibleItem;

    private CollectibleItemType lastType;
    // private Weapon savedWeapon;

    public CollectibleItemType CollectibleItemType => collectibleItemType;
    public CollectibleItem CollectibleItem { get { return collectibleItem; } }  
    
    private bool IsUnityLoaded = false; 


    public void Awake()
    {
        lastType = collectibleItemType;  
        IsUnityLoaded = true;
    }


    public CollectiblesScriptableObject()
    {
        lastType = CollectibleItemType;
    }

    void OnValidate()
    {
        if(collectibleItemType != lastType && IsUnityLoaded)
        { 
            lastType = collectibleItemType;
            collectibleItem = UpdateCollectibleItemType(lastType);
        }
    }

    private CollectibleItem UpdateCollectibleItemType(CollectibleItemType other)
    {
        CollectibleItem newCollectibleItem = null;
        
        switch(other)
        {
             case CollectibleItemType.Ammunation : 
                newCollectibleItem = new Bullets();
                break;

             case CollectibleItemType.Medical : 
                newCollectibleItem = new HealthKit();
                break;
        }
        return newCollectibleItem;
    }

}

 [Serializable]
public class Collectible
{
    public CollectibleItemType CollectibleItemType;

    public CollectibleItem CollectibleItem;

}

[Serializable]
public class CollectibleItem
{
    public int CollectibleAmountContain;
}

[System.Serializable]
public class Bullets : CollectibleItem
{
   public ProjectileType ProjectileType;
}

[System.Serializable]
public class HealthKit : CollectibleItem
{
   public HealthKitType HealthKitType;
}


