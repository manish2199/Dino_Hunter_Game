using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AmmoBox : MonoBehaviour , ICollectable 
{ 
   // Jobs 
   // 1. When Player Press Button it will add available bullets to inventory                 // Completed
   // 2. Listens events for new bullet item add and to increase capacity
   // 3. Tell supplies provide to change the position 
   // 4. Also Show Notification msg to player when inside collider


   [HideInInspector] public List<Bullets> BulletList;
 
   public Bullets revbullet;

   public Bullets shotgunBullet; 

   public Bullets assBullet;

    void Awake()
    {
        BulletList = new List<Bullets>();
    }

    void Start()
    {
        AddBulletToTheList(revbullet);
        AddBulletToTheList(shotgunBullet);
        AddBulletToTheList(assBullet);
    }

    public void Collect()
    {
        // if player press certain key then this will add to inventory system particular item 
        // this item check which weapons are unlocked and according to that supply bullets
        for(int i = 0; i<BulletList.Count; i++)
        {  
            InventoryService.Instance.AddProjectiles(BulletList[i].ProjectileType,BulletList[i].ProjectileAmount); 
            NotificationManager.Instance.ShowNotificationMsg(NotificationType.ItemCollectedNotification);
        }

    }


    public void AddBulletToTheList(Bullets bullet)
    {
       BulletList.Add(bullet);
    }



    void OnTriggerEnter(Collider other)
    {
        var temp = other.gameObject.GetComponent<PlayerHitBox>();

        if(temp != null)
        {
            NotificationManager.Instance.ShowNotificationMsg(NotificationType.CollectItemNotification);
        }
    }


     
    // this ammo box have event which listen t achievement system to increase the capacity or adding new bulllet to collect by player


}



[System.Serializable]
public class Bullets
{
   public ProjectileType ProjectileType;

   public int ProjectileAmount;
}