using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServicePool<T> : GenericSingleton<ServicePool<T>> where T : class
{
   [HideInInspector] protected List<PoolItem<T>> pooledItems =  new List<PoolItem<T>>();

   public virtual T GetItem()
   {
       if(pooledItems.Count > 0)
       {
           PoolItem<T> item = pooledItems.Find( I => I.isUsed == false);
           if(item != null)
           { 
              item.isUsed = true;
              return  item.Item; 
           }
       }
       // create new item for pool
       return createNewPooledItem();
   }  

   protected virtual T createNewPooledItem()
   {
        PoolItem<T> pooledItem = new PoolItem<T>();
        pooledItem.Item = CreateItem();
        pooledItem.isUsed = true;
        pooledItems.Add(pooledItem);  
        return pooledItem.Item;
   }

   protected virtual T CreateItem()
   {
       return (T) null;
   }

   public virtual void ReturnItem(T item)
   {
       PoolItem<T> pooledItem = pooledItems.Find(itm => itm.Item.Equals(item));
       if(pooledItem != null)
       {
       Debug.Log("Item Returned");
       }
        pooledItem.isUsed = false;
   }

   protected class PoolItem<T>
  { 
    public T Item;
    public bool isUsed;
  }    
}


