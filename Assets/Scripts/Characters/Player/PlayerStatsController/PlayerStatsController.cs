using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatsController : MonoBehaviour 
{
   private int PlayerScore;
 
   private int StartingPlayerHealth;

   private int CurrentPlayerHealth; 

   private bool IsMediKitApplying;

   PlayerScriptableObject playerScriptableObject;

   Transform playerCollectibleTransform;


   public void InitializePlayerStats()
   {
       playerScriptableObject = Player.Instance.PlayerScriptableObject;
       playerCollectibleTransform = Player.Instance.PlayerCollectableTransform;
       IsMediKitApplying = false;
       StartingPlayerHealth = playerScriptableObject.PlayerHealth;
       CurrentPlayerHealth = playerScriptableObject.PlayerHealth;
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
 
   public void CheckToUseMedikit()
   {
        if(Input.GetKeyDown(playerScriptableObject.playerControls.KeyToUseHealthKit) && !IsMediKitApplying)
        {
            IsMediKitApplying = true;
            // demand healthkit from inventory 
            if(InventoryService.Instance.GetHealthKit(HealthKitType.FirstAidKit) == 0 )
            {
                return;
            }

           if(CurrentPlayerHealth < StartingPlayerHealth  )
           {   
              CurrentPlayerHealth += InventoryService.Instance.GetHealthKit(HealthKitType.FirstAidKit);

              if(CurrentPlayerHealth > StartingPlayerHealth)
              {
                CurrentPlayerHealth = StartingPlayerHealth;
              }

              float damageLeft = (StartingPlayerHealth -  CurrentPlayerHealth );

              GameplayUIManager.Instance.UpdateDamageIndicator(damageLeft);
           }
            IsMediKitApplying = false;
        }
   }


   public void CheckToCollectSupplies()
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

   public void CheckToOpenInventory()
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
