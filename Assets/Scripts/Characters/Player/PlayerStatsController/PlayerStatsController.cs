using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class PlayerStatsController : MonoBehaviour
// {
//    private int PlayerScore;
 
//    private int StartingPlayerHealth;

//    private int CurrentPlayerHealth;

// //    void Start()
// //    {
// //        StartingPlayerHealth = Player.Instance.PlayerScriptableObject.PlayerHealth;
// //        CurrentPlayerHealth = StartingPlayerHealth;
// //    }

//    public void InitializePlayerStats()
//    {
//         CurrentPlayerHealth = StartingPlayerHealth;
//    }
  

// //    void Update()
// //    {
// //       OpenInventorySystem();

// //       CollectFromSupplies();

// //       UseMedikit();
// //    }


// //    void PlayerStatsUpdates()
// //    {
//     //   OpenInventorySystem();

//     //   CollectFromSupplies();

//     //   UseMedikit();
// //    }

//    public void ResetPlayerHealth()
//    {
//         CurrentPlayerHealth = StartingPlayerHealth;
//         GameplayUIManager.Instance.UpdateDamageIndicator(0); 
//    }

//    public void ResetScore()
//    {
//        PlayerScore = 0;
//    }
 
//    private void UseMedikit(KeyCode keyCode)
//    {
//         // if(Input.GetKeyDown(Player.Instance.PlayerScriptableObject.playerControls.KeyToUseHealthKit))
//         if(Input.GetKeyDown(keyCode))
//         {
//             // demand healthkit from inventory 
//             if(InventoryService.Instance.GetHealthKit(HealthKitType.FirstAidKit) == 0 )
//             {
//                 return;
//             }

//            if(CurrentPlayerHealth < StartingPlayerHealth )
//            {   
//               CurrentPlayerHealth += InventoryService.Instance.GetHealthKit(HealthKitType.FirstAidKit);

//               if(CurrentPlayerHealth > StartingPlayerHealth)
//               {
//                 CurrentPlayerHealth = StartingPlayerHealth;
//               }

//               float damageLeft = (StartingPlayerHealth -  CurrentPlayerHealth );

//               GameplayUIManager.Instance.UpdateDamageIndicator(damageLeft);
//            }
//         }
//    }


//    public void CollectFromSupplies(Transform playerCollectibleTransform , KeyCode keyCode)
//    {
//        RaycastHit hit;

//        if(Physics.Raycast(playerCollectibleTransform.position,playerCollectibleTransform.forward, out hit,3f))
//        {
//            ICollectable collectable = hit.transform.gameObject.GetComponent<ICollectable>();

//         //    if(collectable != null && Input.GetKeyDown(Player.Instance.PlayerScriptableObject.playerControls.KeyToInteractWithObjects))
//            if(collectable != null && Input.GetKeyDown(keyCode))
//            { 
//               collectable.Collect();
//            }
//        }
   
//    }

//    public void IncreaseScore(WalkingDinosaurType walkableDinosaurType)
//    {
//        if(walkableDinosaurType == WalkingDinosaurType.Raptors)
//        {
//            PlayerScore += 20;
//        }
//        if(walkableDinosaurType == WalkingDinosaurType.TRex)
//        {
//            PlayerScore += 40;
//        }

//        GameplayUIManager.Instance.UpdatePlayerScore(PlayerScore);

//        if(GameData.GetEasyDifficulty() == 1)
//        {
//           if( PlayerScore > GameData.GetEasyDifficultyHighScore())
//           {
//             GameData.SetEasyDifficultyHighScore(PlayerScore);
//           }
//        }
//        if(GameData.GetMediumDifficulty() == 1)
//        {
//           if( PlayerScore > GameData.GetMediumDifficultyHighScore())
//           {
//             GameData.SetMediumDifficultyHighScore(PlayerScore);
//           }
//        }
//        if(GameData.GetHardDifficulty() == 1)
//        {
//           if( PlayerScore > GameData.GetHardDifficultyHighScore())
//           {
//             GameData.SetHardDifficultyHighScore(PlayerScore);
//           }
//        }
  
//    }




//    public void OpenInventorySystem(KeyCode keyCode)
//    {  
//     //    if(Input.GetKeyDown(Player.Instance.PlayerScriptableObject.playerControls.KeyToOpenInventory))
//        if(Input.GetKeyDown(keyCode))
//       {
//         GameplayUIManager.Instance.ActivateInventory();
//       }
//    }



//     public void TakeDamage(int damage)
//     {
//        CurrentPlayerHealth -= damage;
      
//        float damageDealed = (StartingPlayerHealth - CurrentPlayerHealth );

//        GameplayUIManager.Instance.UpdateDamageIndicator(damageDealed);

//        if(CurrentPlayerHealth <= 0)
//        {
//             GamePlayManager.Instance.PlayerDied();
//        }

//     }
// }



public class PlayerStatsController : MonoBehaviour 
{
   private int PlayerScore;
 
   private int StartingPlayerHealth;

   private int CurrentPlayerHealth;


   public void InitializePlayerStats(PlayerScriptableObject playerScriptableObject)
   {
       StartingPlayerHealth = playerScriptableObject.PlayerHealth;
       CurrentPlayerHealth = StartingPlayerHealth;
   }
  

   public void ResetPlayerHealth()
   {
        CurrentPlayerHealth = StartingPlayerHealth;
        GameplayUIManager.Instance.UpdateDamageIndicator(0); 
   }

   public void ResetScore()
   {
       PlayerScore = 0;
   }
 
   public void CheckToUseMedikit(PlayerScriptableObject playerScriptableObject)
   {
        if(Input.GetKeyDown(playerScriptableObject.playerControls.KeyToUseHealthKit))
        {
            // demand healthkit from inventory 
            if(InventoryService.Instance.GetHealthKit(HealthKitType.FirstAidKit) == 0 )
            {
                return;
            }

           if(CurrentPlayerHealth < StartingPlayerHealth )
           {   
              CurrentPlayerHealth += InventoryService.Instance.GetHealthKit(HealthKitType.FirstAidKit);

              if(CurrentPlayerHealth > StartingPlayerHealth)
              {
                CurrentPlayerHealth = StartingPlayerHealth;
              }

              float damageLeft = (StartingPlayerHealth -  CurrentPlayerHealth );

              GameplayUIManager.Instance.UpdateDamageIndicator(damageLeft);
           }
        }
   }


   public void CheckToCollectSupplies(Transform playerCollectibleTransform,PlayerScriptableObject playerScriptableObject)
   {
       RaycastHit hit;

       if(Physics.Raycast(playerCollectibleTransform.position,playerCollectibleTransform.forward, out hit,3f))
       {
           ICollectable collectable = hit.transform.gameObject.GetComponent<ICollectable>();

           if(collectable != null && Input.GetKeyDown(playerScriptableObject.playerControls.KeyToInteractWithObjects))
           { 
              collectable.Collect();
           }
       }
   
   }

   public void IncreaseScore(WalkingDinosaurType walkableDinosaurType)
   {
       if(walkableDinosaurType == WalkingDinosaurType.Raptors)
       {
           PlayerScore += 20;
       }
       if(walkableDinosaurType == WalkingDinosaurType.TRex)
       {
           PlayerScore += 40;
       }

       GameplayUIManager.Instance.UpdatePlayerScore(PlayerScore);

       if(GameData.GetEasyDifficulty() == 1)
       {
          if( PlayerScore > GameData.GetEasyDifficultyHighScore())
          {
            GameData.SetEasyDifficultyHighScore(PlayerScore);
          }
       }
       if(GameData.GetMediumDifficulty() == 1)
       {
          if( PlayerScore > GameData.GetMediumDifficultyHighScore())
          {
            GameData.SetMediumDifficultyHighScore(PlayerScore);
          }
       }
       if(GameData.GetHardDifficulty() == 1)
       {
          if( PlayerScore > GameData.GetHardDifficultyHighScore())
          {
            GameData.SetHardDifficultyHighScore(PlayerScore);
          }
       }
   }

   public void CheckToOpenInventory(PlayerScriptableObject playerScriptableObject)
   {  
       if(Input.GetKeyDown(playerScriptableObject.playerControls.KeyToOpenInventory))
      {
        GameplayUIManager.Instance.ActivateInventory();
      }
   }


    public void TakeDamage(int damage)
    {
       CurrentPlayerHealth -= damage;
      
       float damageDealed = (StartingPlayerHealth - CurrentPlayerHealth );

       GameplayUIManager.Instance.UpdateDamageIndicator(damageDealed);

       if(CurrentPlayerHealth <= 0)
       {
            GamePlayManager.Instance.PlayerDied();
       }

    }
}
