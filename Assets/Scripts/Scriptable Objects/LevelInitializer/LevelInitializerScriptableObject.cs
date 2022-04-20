using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewLevelInitializerScriptableObjectList")]
public class LevelInitializerScriptableObject : ScriptableObject
{
    public InventoryItemConstraints[] InventoryBulletsLists;

    public InventoryItemConstraints[] InventoryMedicalKitLists;

    public CollectiblesScriptableObject[] CollectibleProjectiles;

    public CollectiblesScriptableObject[] CollectibleHealthKits;
}



[System.Serializable]
public class InventoryItemConstraints
{
   public InventoryItem InventoryItem;
   public int InitialQuantity;
   public int InitialMaxLimit; 
}

