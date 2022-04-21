using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryItem : ScriptableObject
{
   [SerializeField] protected Sprite ItemIcon;

   [SerializeField] protected InventoryItemType itemType;
   
   public Sprite UIIcon { get { return ItemIcon; } }
   
   public InventoryItemType InventoryItemType { get { return itemType; } }
       
}


[Serializable]
public class ItemSlot
{
    private int Quantity;
    private int MaxLimit;
    private InventoryItem Item;

    public void SetQuantity(int Quantity)
    {
        this.Quantity += Quantity;
    }

    public int GetMaxQuanity()
    {
        return MaxLimit;
    }

    public void UpdateMaxLimit(int maxLimit)
    {
        MaxLimit = maxLimit;
    }

    public int GetQuantity()
    {
        return Quantity;
    }

    public void ReduceQuantity()
    {
        Quantity = Quantity -1;
    }


    public void SetItem(InventoryItem Item)
    {
        this.Item = Item;
    }

    public InventoryItem InventoryItem { get{ return Item; } }
}


public enum HealthKitType
{
    None, 
    FirstAidKit,
}


public enum InventoryItemType
{
    None, 
    WeaponProjectile,
    HealthKit
}

