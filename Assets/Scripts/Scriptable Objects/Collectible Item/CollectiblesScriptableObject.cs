using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewCollectibleScriptableObject")]
public class CollectiblesScriptableObject : ScriptableObject
{

    [SerializeField] CollectibleItemType collectibleItemType;
    [SerializeReference] CollectibleItem collectibleItem;

    private CollectibleItemType lastType;

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


