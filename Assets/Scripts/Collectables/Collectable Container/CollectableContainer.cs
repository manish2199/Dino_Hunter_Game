﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class CollectableContainer : MonoBehaviour , ICollectable 
{ 
    // Just for Temporary use 
     
   public CollectableContainerType collectableContainerType;
//    medical box provide medikit only 
// ammo box provide ammo only 
 

    private List<Collectible> CollectibleList;  

    public Transform[] SpawningPositions;

    private int CurrentSpawnIndex = 0;

    public int TimeToRespawn;

    void Awake()
    {
        CollectibleList = new List<Collectible>();
    }

    void OnEnable()
    {
       // If Achievement Complete the add item to list (sUBS)
    }

    void OnDisable()
    {
       //(uNSUBS)
    }

    void Start()
    {
       
        // handle by gameplay manager
        // AddCollectibleToTheList(revolverBulletsCollecetible);
    }

    public async void Collect()
    {
        // if player press certain key then this will add to inventory system particular item 
        // this item check which item are unlocked and according to that supply items
        for(int i = 0; i<CollectibleList.Count; i++)
        {  
            InventoryService.Instance.AddItemFromSupplies(CollectibleList[i].CollectibleItemType,CollectibleList[i].CollectibleItem); 
            if(NotificationManager.Instance != null)
            {
               NotificationManager.Instance.ShowNotificationMsg(NotificationType.ItemCollectedNotification);
            }
        }
        await StartRespawnTimer();
    }


    async Task StartRespawnTimer()
    {
        await Task.Delay(1000);
                
        IterateRandomPosition();
    
        await Task.Delay(TimeToRespawn);
        
        SpawnOnRandomPosition();
    }

    private void IterateRandomPosition()
    {
       int prevSpawnIndex = CurrentSpawnIndex;
       do
       {
         CurrentSpawnIndex = Random.Range(0,SpawningPositions.Length-1);
       }while(prevSpawnIndex == CurrentSpawnIndex);
    }

    private void SpawnOnRandomPosition()
    {
        transform.position = SpawningPositions[CurrentSpawnIndex].position;
    }

    public void AddCollectibleToTheList(Collectible collectible)
    {
       CollectibleList.Add(collectible);
    }

    void OnTriggerEnter(Collider other)
    {
        var temp = other.gameObject.GetComponent<PlayerHitBox>();

        if(temp != null)
        {
            NotificationManager.Instance.ShowNotificationMsg(NotificationType.CollectItemNotification);
        }
    }
}


public enum CollectableContainerType
{
    None,
    AmmoBox,
    MedicBox
}
 

public enum CollectibleItemType
{
    None,
    Ammunation,
    Medical
}
 